using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.Resources;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ManagmentSystem.EF.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public UserService(IUnitOfWork unitOfWork, IWebHostEnvironment env, IStringLocalizer<SharedResource> localizer)
        {
            _unitOfWork = unitOfWork;
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _localizer = localizer;
        }
        public async Task<AuthModel> AddUser(VMUserPost user)
        {
            try
            {
                if (await _unitOfWork.Users.FindByEmailAsync(user.Email) is not null)
                    return new AuthModel { Result = 0, Message = _localizer["EmailIsAlreadyRegistered"] };

                if (await _unitOfWork.Users.FindByUserNameAsync(user.UserName) is not null)
                    return new AuthModel { Result = 0, Message = _localizer["UserNameIsAlreadyRegistered"] };

                var PasswordHashed = "";
                if (!string.IsNullOrEmpty(user.Password))
                {
                    string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
                    if (!Regex.IsMatch(user.Password, passwordRegex))
                    {
                        return new AuthModel { Result = 3, Message = _localizer["ValidatePassword"] };
                    }
                    if (user.Password.Length > 25)
                    {
                        return new AuthModel { Result = 3, Message = _localizer["PasswordToolLong"] };
                    }
                    if (user.Password.Length < 8)
                    {
                        return new AuthModel { Result = 3, Message = _localizer["PasswordTooShort"] };
                    }
                    var hasher = new PasswordHasher<VMUserPost>();
                    PasswordHashed = hasher.HashPassword(user, user.Password);
                }
                else
                {
                    PasswordHashed = "Sys_Pass_Temp";
                }

                var userObj = new User
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    AFirstName = user.AFirstName,
                    EFirstName = user.EFirstName,
                    ALastName = user.ALastName,
                    ELastName = user.ELastName,
                    PhoneNumber = user.PhoneNumber,
                    BlockedType = user.BlockedType,
                    IsAdmin = user.IsAdmin,
                    IsBlocked = user.IsBlocked,
                    Password = PasswordHashed,
                    UserType = user.UserType
                };

                var addedUser = await _unitOfWork.Users.Add(userObj);

                var userPositionObj = new UserPosition
                {
                    UserName = addedUser.UserName,
                    UserId = addedUser.Id,
                    PositionId = user.PositionId,
                    IsActive = true,
                    Type = (int)VMUserPosition.Enum_UserPosition_Type.HR,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(12)
                };

                var UserPosition = await _unitOfWork.UserPositions.Add(userPositionObj);

                await _unitOfWork.CompleteAsync();

                if (UserPosition != null)
                {
                    return new AuthModel { Result = 1 };
                }
                else
                {
                    return new AuthModel { Result = 0, Message = _localizer["Failed"] };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> CheckExistEmailForUpdate(string Email, string id)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(i => i.Email == Email && i.Id != id);
                if (user != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> CheckExistUserNameForUpdate(string UserName, string id)
        {
            try
            {
                var user = await _unitOfWork.Users.FindAsync(i => i.UserName == UserName && i.Id != id);
                if (user != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> DeleteUser(string Id)
        {
            try
            {
                var User = _unitOfWork.Users.Delete(Id);
                if (User != 0)
                {
                    await _unitOfWork.CompleteAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<VMUser> GetUser(string Id)
        {
            try
            {
                var User = await _unitOfWork.Users.GetByIdAsync(Id);
                var UserTypeName = "";
                UserTypeName = User.UserType switch
                {
                    (int)VMUser.Enum_User_Type.Employee => (string)_localizer["Employee"],
                    (int)VMUser.Enum_User_Type.Producer => (string)_localizer["Producer"],
                    (int)VMUser.Enum_User_Type.Consumer => (string)_localizer["Consumer"],
                    _ => "",
                };

                Expression<Func<UserPosition, bool>> match2 = u => (u.UserId == Id && u.Type == (int)VMUserPosition.Enum_UserPosition_Type.HR);
                var UserPosition = await _unitOfWork.UserPositions.FindAsync(match2);

                var Position = await _unitOfWork.Positions.GetByIdAsync(UserPosition.PositionId);

                var Department = await _unitOfWork.Departments.GetByIdAsync(Position.DepartmentId);


                return new VMUser(User.Id, User.UserName, User.AFirstName, User.EFirstName, User.ALastName, User.ELastName,
                    User.Email, User.PhoneNumber, User.IsBlocked, User.IsAdmin, User.BlockedType, User.UserType, UserTypeName,
                    Department.Id, Department.Code, Department.EName, Department.AName, Position.Id, Position.AName, Position.EName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<(IEnumerable<VMUser> users, int totalItems)> GetUsersAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null)
        {
            try
            {
                search ??= new Dictionary<string, string>();
                Expression<Func<User, bool>> match = u =>
                        (string.IsNullOrEmpty(search.GetValueOrDefault("userName")) || u.UserName.Contains(search["userName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("aFirstName")) || u.AFirstName.Contains(search["aFirstName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("eFirstName")) || u.EFirstName.Contains(search["eFirstName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("aLastName")) || u.ALastName.Contains(search["aLastName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("eLastName")) || u.ELastName.Contains(search["eLastName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("userType")) || u.UserType.Equals(search["userType"])) &&
                        (!search.ContainsKey("isBlocked") || string.IsNullOrEmpty(search["isBlocked"]) || u.IsBlocked == Convert.ToBoolean(search["isBlocked"])) &&
                        (!search.ContainsKey("isAdmin") || string.IsNullOrEmpty(search["isAdmin"]) || u.IsAdmin == Convert.ToBoolean(search["isAdmin"]));

                int totalItems = await _unitOfWork.Users.CountAsync(match);
                int countItems = await _unitOfWork.Users.CountAllAsync();

                int skip = (page - 1) * pageSize;
                int take = pageSize;

                var users = await _unitOfWork.Users.FindAllAsync(match, take, skip, sort);
                var UsersResult = new List<VMUser>();
                foreach (var User in users)
                {
                    var UserTypeName = "";
                    UserTypeName = User.UserType switch
                    {
                        (int)VMUser.Enum_User_Type.Employee => (string)_localizer["Employee"],
                        (int)VMUser.Enum_User_Type.Producer => (string)_localizer["Producer"],
                        (int)VMUser.Enum_User_Type.Consumer => (string)_localizer["Consumer"],
                        _ => "",
                    };
                    UsersResult.Add(new VMUser(User.Id, User.UserName, User.AFirstName, User.EFirstName, User.ALastName, User.ELastName,
                    User.Email, User.PhoneNumber, User.IsBlocked, User.IsAdmin, User.BlockedType, User.UserType, UserTypeName, "", "", "", "", "", "", ""));
                }
                return (UsersResult, totalItems);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AuthModel> UpdateUser(VMUserPost user)
        {
            try
            {
                var UserCurr = await _unitOfWork.Users.GetByIdAsync(user.Id);
                Expression<Func<UserPosition, bool>> match2 = u => (u.UserId == user.Id && u.Type == (int)VMUserPosition.Enum_UserPosition_Type.HR);
                var UserPositionCurr = await _unitOfWork.UserPositions.FindAsync(match2);

                if (UserCurr == null)
                    return new AuthModel { Result = 0, Message = _localizer["UserNotFound"] };

                var UserCurrByEmail = await CheckExistEmailForUpdate(user.Email, user.Id);
                var UserCurrByUserName = await CheckExistUserNameForUpdate(user.UserName, user.Id);

                if (UserCurrByEmail)
                    return new AuthModel { Result = 0, Message = _localizer["EmailIsAlreadyRegistered"] };

                if (UserCurrByUserName)
                    return new AuthModel { Result = 0, Message = _localizer["UserNameIsAlreadyRegistered"] };

                if (!string.IsNullOrEmpty(user.Password))
                {
                    string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
                    if (!Regex.IsMatch(user.Password, passwordRegex))
                    {
                        return new AuthModel { Result = 0, Message = _localizer["ValidatePassword"] };
                    }
                    if (user.Password.Length > 25)
                    {
                        return new AuthModel { Result = 0, Message = _localizer["PasswordToolLong"] };
                    }
                    if (user.Password.Length < 8)
                    {
                        return new AuthModel { Result = 0, Message = _localizer["PasswordTooShort"] };
                    }

                    var hasher = new PasswordHasher<VMUserPost>();
                    UserCurr.Password = hasher.HashPassword(user, user.Password);
                }

                UserCurr.UserName = user.UserName;
                UserCurr.Email = user.Email;
                UserCurr.AFirstName = user.AFirstName;
                UserCurr.EFirstName = user.EFirstName;
                UserCurr.ALastName = user.ALastName;
                UserCurr.ELastName = user.ELastName;
                UserCurr.PhoneNumber = user.PhoneNumber;
                UserCurr.BlockedType = user.BlockedType;
                UserCurr.IsAdmin = user.IsAdmin;
                UserCurr.IsBlocked = user.IsBlocked;
                UserCurr.UserType = user.UserType;

                if (!UserPositionCurr.PositionId.Equals(user.PositionId))
                {
                    UserPositionCurr.PositionId = user.PositionId;
                    UserPositionCurr.StartDate = DateTime.Now;
                    UserPositionCurr.EndDate = DateTime.Now.AddMonths(12);
                    _unitOfWork.UserPositions.Update(UserPositionCurr);
                }

                _unitOfWork.Users.Update(UserCurr);
                await _unitOfWork.CompleteAsync();
                return new AuthModel { Result = 1 };
            }
            catch (Exception ex)
            {
                return new AuthModel { Result = 0, Message = _localizer["Error"] + ex.Message };
            }
        }
    }
}
