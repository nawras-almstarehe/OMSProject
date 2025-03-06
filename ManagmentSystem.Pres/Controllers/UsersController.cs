using ManagmentSystem.Core.UnitOfWorks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Authorization;
using ManagmentSystem.Core.IServices;
using ManagmentSystem.EF.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Security.Claims;

namespace ManagmentSystem.Pres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IAuthService authService, IUserService userService, ILogger<UsersController> logger)
        {
            _authService = authService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("GetUsers")]
        public async Task<IActionResult> GetUsers([FromBody] VMObjectPost UserPost)
        {
            // 1. Access the User property from HttpContext
            var claims = User.Claims;

            // 2. Extract specific claims
            var username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; // Or the specific claim you used for User ID

            // Example of accessing all claims
            var allClaims = claims.Select(c => new { Type = c.Type, Value = c.Value }).ToList();
            if (UserPost == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var (users, totalItems) = await _userService.GetUsersAll(UserPost.page, UserPost.pageSize, UserPost.filter, UserPost.sort);
                _logger.LogInformation("Test log", users);
                return Ok(new VMResaultDataList<VMUser>(totalItems, users));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string Id)
        {
            if (Id == null || Id == "")
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var User = await _userService.GetUser(Id);
                _logger.LogInformation("Test log", User);
                return Ok(User);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] VMUserPost user)
        {
            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _userService.AddUser(user);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] VMUserPost user)
        {
            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _userService.UpdateUser(user);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null || id == "")
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _userService.DeleteUser(id);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] VMLogin user)
        {
            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var Result = new AuthModel();
                Result =  await _authService.LoginAsync(user);
                _logger.LogInformation("Test log", Result);
                return Ok(VMAllResultApi<object>.SuccessResult("Products retrieved successfully.", Result));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //Invalidate the authentication cookie
            //HttpContext.Session.Clear(); // Clear session data
                                         //Potentially add code here to revoke refresh tokens
            return Ok(new { message = "Logged out successfully." });
        }
    }
}
