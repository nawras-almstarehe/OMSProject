using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using ManagmentSystem.Core.Consts;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IdentityModel.Tokens.Jwt;
using ManagmentSystem.Core.IServices;
using Microsoft.EntityFrameworkCore;

namespace ManagmentSystem.EF.Repositories
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        private new readonly ApplicationDBContext _context;
        private readonly IUnitOfWork _unitOfWork;
        
        public UsersRepository(ApplicationDBContext context) : base(context)
        {
            
        }
        
        public UsersRepository(ApplicationDBContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<VMResult> RegisterLocalAsync(VMRegister model)
        {
            try
            {
                if (await FindByEmailAsync(model.Username) is not null)
                    return new VMResult { Message = "Email is already registered!" };

                if (await FindByUserNameAsync(model.Username) is not null)
                    return new VMResult { Message = "Username is already registered!" };

                var user = new User
                {
                    UserName = model.Username,
                    AFirstName = model.AFirstName,
                    EFirstName = model.EFirstName,
                    ALastName = model.ALastName,
                    ELastName = model.ELastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserType = model.UserType,
                    IsAdmin = model.IsAdmin,
                    IsBlocked = model.IsBlocked,
                };

                var result = await CreateUserAsync(user, model.Password);

                if (!result.Successed)
                {
                    var errors = string.Empty;
                    errors = result.Message;
                    return new VMResult { Message = errors };
                }

                return new VMResult
                {
                    Successed = true,
                    Code = Const.cnstSuccessResultApi
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<VMResult> CreateUserAsync(User user, string password)
        {
            try
            {
                var hasher = new PasswordHasher<User>();
                var result = new VMResult();
                if (user == null || string.IsNullOrEmpty(password))
                {
                    result.Successed = false;
                    return result;
                }
                user.Password = hasher.HashPassword(user, password);
                await _context.Set<User>().AddAsync(user);
                result.Successed = true;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> FindByEmailAsync(string Email)
        {
            try
            {
                var user = new User();
                if (string.IsNullOrEmpty(Email))
                {
                    return null;
                }
                user = await FindAsync(x => x.Email == Email);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> FindByUserNameAsync(string Username)
        {
            try
            {
                var user = new User();
                if (string.IsNullOrEmpty(Username))
                {
                    return null;
                }
                user = await FindAsync(x => x.UserName == Username);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<double> GetUserPrivilegesAsync(string UserId)
        //{
        //    try
        //    {
        //        var user = new User();
        //        var userPosition = new UserPosition();
        //        if (string.IsNullOrEmpty(UserId))
        //        {
        //            return (double)(VMPrivilege.Enum_Privilege.None);
        //        }
        //        userPosition = await _unitOfWork.UserPositions.GetUserPositionByUserId(UserId);
        //        user = await _unitOfWork.Users.FindAsync(x => x.UserName == Username);
        //        return user;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
