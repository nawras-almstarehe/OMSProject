using ManagmentSystem.Core.UnitOfWorks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Authorization;

namespace ManagmentSystem.Pres.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Register")]
        public async Task<IActionResult> Register([FromBody] VMRegister user)
        {
            try
            {
                var Result = new VMResult();
                Result = await _unitOfWork.Users.RegisterLocalAsync(user);
                return Ok(Result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromBody] VMLogin user)
        {
            try
            {
                var Result = new AuthModel();
                Result =  await _unitOfWork.Users.LoginAsync(user);
                return Ok(Result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
