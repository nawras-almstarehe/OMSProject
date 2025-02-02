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

namespace ManagmentSystem.Pres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IAuthService authService, ILogger<UsersController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        //[HttpGet("Register")]
        //public async Task<IActionResult> Register([FromBody] VMRegister user)
        //{
        //    try
        //    {
        //        var Result = new VMResult();
        //        Result = await _unitOfWork.Users.RegisterLocalAsync(user);
        //        return Ok(Result);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] VMLogin user)
        {
            try
            {
                var Result = new AuthModel();
                Result =  await _authService.LoginAsync(user);
                _logger.LogInformation("Test log", Result);
                return Ok(VMAllResultApi<object>.SuccessResult("Products retrieved successfully.", Result));
                //return Ok(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return BadRequest(VMAllResultApi<object>.FailureResult($"An error occurred: {ex.Message}"));
                //throw;
            }
        }
    }
}
