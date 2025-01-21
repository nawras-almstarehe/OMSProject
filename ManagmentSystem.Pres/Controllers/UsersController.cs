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

namespace ManagmentSystem.Pres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        public UsersController(IAuthService authService)
        {
            _authService = authService;
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
                return Ok(Result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
