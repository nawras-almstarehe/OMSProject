using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public UserService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }
        public async Task<AuthModel> AddUser(User user)
        {
            try
            {
                if (await _unitOfWork.Users.FindByEmailAsync(user.Email) is not null)
                    return new AuthModel { Result = 0, Message = "Email is already registered!" };

                if (await _unitOfWork.Users.FindByUserNameAsync(user.UserName) is not null)
                    return new AuthModel { Result = 0, Message = "Username is already registered!" };

                var PasswordHashed = "";
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var hasher = new PasswordHasher<User>();
                    PasswordHashed = hasher.HashPassword(user, user.Password);
                }
                else
                {
                    PasswordHashed = "Sys_Pass_Temp";
                }

                var userObj = new User { 
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
                var User = await _unitOfWork.Users.Add(userObj);
                if (User != null)
                {
                    await _unitOfWork.CompleteAsync();
                    return new AuthModel { Result = 1};
                }
                else
                {
                    return new AuthModel { Result = 0, Message = "Failed" };
                }

            }
            catch (Exception)
            {
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
                //var images = _unitOfWork.Images.FindAll(i => i.User == Id);
                //if (images.Any())
                //{
                //    foreach (var image in images)
                //    {
                //        var filePath = Path.Combine(_env.WebRootPath, image.ImagePath.TrimStart('/'));
                //        if (System.IO.File.Exists(filePath))
                //        {
                //            System.IO.File.Delete(filePath);
                //        }
                //    }
                //    _unitOfWork.Images.DeleteRange(images);
                //    await _unitOfWork.CompleteAsync();
                //}

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
                return new VMUser(User.Id,User.UserName, User.AFirstName, User.EFirstName, User.ALastName, User.ELastName,
                    User.Email, User.PhoneNumber, User.IsBlocked, User.IsAdmin, User.BlockedType, User.UserType);
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
                    UsersResult.Add(new VMUser(User.Id, User.UserName, User.AFirstName, User.EFirstName, User.ALastName, User.ELastName,
                    User.Email, User.PhoneNumber, User.IsBlocked, User.IsAdmin, User.BlockedType, User.UserType));
                }
                return (UsersResult, totalItems);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthModel> UpdateUser(User user)
        {
            try
            {
                var UserCurr = await _unitOfWork.Users.GetByIdAsync(user.Id);
                if (UserCurr == null)
                    return new AuthModel { Result = 0, Message = "User not found!" };

                var UserCurrByEmail = await CheckExistEmailForUpdate(user.Email, user.Id);
                var UserCurrByUserName = await CheckExistUserNameForUpdate(user.UserName, user.Id);

                if (UserCurrByEmail)
                    return new AuthModel { Result = 0, Message = "Email is already registered!" };

                if (UserCurrByUserName)
                    return new AuthModel { Result = 0, Message = "Username is already registered!" };

                if (!string.IsNullOrEmpty(user.Password))
                {
                    var hasher = new PasswordHasher<User>();
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

                _unitOfWork.Users.Update(UserCurr);
                await _unitOfWork.CompleteAsync();
                return new AuthModel { Result = 1 };
            }
            catch (Exception ex)
            {
                return new AuthModel { Result = 0, Message = $"Error: {ex.Message}" };
            }
        }
    }
}
