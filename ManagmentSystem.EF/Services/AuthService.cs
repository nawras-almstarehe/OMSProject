using ManagmentSystem.Core.Helpers;
using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.Resources;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JWT _jwt;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public AuthService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IStringLocalizer<SharedResource> localizer)
        {
            _unitOfWork = unitOfWork;
            _jwt = jwt.Value;
            _localizer = localizer;
        }
        public async Task<AuthModel> LoginAsync(VMLogin model)
        {
            var authModel = new AuthModel();
            var user = await _unitOfWork.Users.FindByUserNameAsync(model.Username);
            if (user is null || user.Password == "Sys_Pass_Temp" || !VerifyPassword(user, model.Password))
            {
                throw new ServiceException(_localizer["UsernameOrPasswordIsIncorrect"], 400);
            }
            double PrivilegeCode = await _unitOfWork.UserPositions.GetUserPrivilegesAsync(user.Id);
            var jwtSecurityToken = CreateJwtToken(user, PrivilegeCode);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            return authModel;
        }
        private JwtSecurityToken CreateJwtToken(User user, double privilegeCode)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("privilegeCode", privilegeCode.ToString()),
                new Claim("aFullName", user.AFirstName.ToString() + " " + user.ALastName.ToString()),
                new Claim("eFullName", user.EFirstName.ToString() + " " + user.ELastName.ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthModel> RegisterAsync(VMRegister model)
        {
            try
            {
                if (await _unitOfWork.Users.FindByEmailAsync(model.Email) is not null)
                    return new AuthModel { Message = "Email is already registered!" };

                if (await _unitOfWork.Users.FindByUserNameAsync(model.Username) is not null)
                    return new AuthModel { Message = "Username is already registered!" };

                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _unitOfWork.Users.CreateUserAsync(user, model.Password);

                if (!result.Successed)
                {
                    var errors = string.Empty;
                    errors = result.Message;
                    return new AuthModel { Message = errors };
                }

                //var jwtSecurityToken = await CreateJwtToken(user);

                return new AuthModel
                {
                    Email = user.Email,
                    Username = user.UserName
                };
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool VerifyPassword(User user, string Password)
        {
            var hasher = new PasswordHasher<User>();
            var UserPassword = user.Password;
            var result = hasher.VerifyHashedPassword(user, UserPassword, Password);
            bool isPasswordValid = result == PasswordVerificationResult.Success;
            return isPasswordValid;
        }
    }
}
