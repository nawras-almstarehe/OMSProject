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
    public class UserPositionsController : ControllerBase
    {
        private readonly IUserPositionService _userPositionService;
        private readonly ILogger<UserPositionsController> _logger;
        public UserPositionsController(IUserPositionService userPositionService, ILogger<UserPositionsController> logger)
        {
            _userPositionService = userPositionService;
            _logger = logger;
        }

        [HttpPost("GetAssignments")]
        public async Task<IActionResult> GetAssignments([FromBody] VMObjectPost UserPositionPost)
        {
            if (UserPositionPost == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var (userPositions, totalItems) = await _userPositionService.GetUserPositionsAll(UserPositionPost.page, UserPositionPost.pageSize, UserPositionPost.filter, UserPositionPost.sort);
                _logger.LogInformation("Test log", userPositions);
                return Ok(new VMResaultDataList<VMUserPosition>(totalItems, userPositions));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetAssignment")]
        public async Task<IActionResult> GetAssignment(string Id)
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
                var UserPosition = await _userPositionService.GetUserPosition(Id);
                _logger.LogInformation("Test log", UserPosition);
                return Ok(UserPosition);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddAssignment")]
        public async Task<IActionResult> AddAssignment([FromBody] UserPosition userPosition)
        {
            if (userPosition == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _userPositionService.AddAssignment(userPosition);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdateAssignment")]
        public async Task<IActionResult> UpdateAssignment([FromBody] UserPosition userPosition)
        {
            if (userPosition == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _userPositionService.UpdateAssignment(userPosition);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("DeleteAssignment")]
        public async Task<IActionResult> DeleteAssignment(string id)
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
                var result = await _userPositionService.DeleteAssignment(id);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
