using ManagmentSystem.Core.Helpers;
using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AuthService(IUnitOfWork unitOfWork, IOptions<JWT> jwt)
        {
            _unitOfWork = unitOfWork;
            _jwt = jwt.Value;
        }
        public async Task<AuthModel> LoginAsync(VMLogin model)
        {
            var authModel = new AuthModel();

            var user = await _unitOfWork.Users.FindByUserNameAsync(model.Username);

            if (user is null || !VerifyPassword(user, model.Password))
            {
                authModel.Message = "Username or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var PrivilegeCode = await _unitOfWork.UserPositions.GetUserPrivilegesAsync(user.Id);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.PriviligeCode = PrivilegeCode;

            return authModel;
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

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            double privilegeCode = await _unitOfWork.UserPositions.GetUserPrivilegesAsync(user.Id);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("privilegeCode", privilegeCode.ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public bool VerifyPassword(User user, string Password)
        {
            var hasher = new PasswordHasher<User>();
            var UserPassword = user.Password;
            var result = hasher.VerifyHashedPassword(null, UserPassword, Password);
            bool isPasswordValid = result == PasswordVerificationResult.Success;
            return isPasswordValid;
            //return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }
    }
}
