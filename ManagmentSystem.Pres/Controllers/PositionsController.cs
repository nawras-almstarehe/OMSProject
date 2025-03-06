using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using ManagmentSystem.EF.Services;
using ManagmentSystem.Pres.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly ILogger<PositionsController> _logger;
        public PositionsController(IPositionService positionService, ILogger<PositionsController> logger)
        {
            _positionService = positionService;
            _logger = logger;
        }

        [HttpPost("GetPositions")]
        public async Task<IActionResult> GetPositions([FromBody] VMObjectPost PositionPost)
        {
            if (PositionPost == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var (positions, totalItems) = await _positionService.GetPositionsAll(PositionPost.page, PositionPost.pageSize, PositionPost.filter, PositionPost.sort);
                _logger.LogInformation("Test log", positions);
                return Ok(new VMResaultDataList<Position>(totalItems, positions));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetPositionsList")]
        public async Task<IActionResult> GetPositionsList(string departmentId, string filter)
        {
            try
            {
                var positions = await _positionService.GetPositionsList(departmentId, filter);
                _logger.LogInformation("Test log", positions);
                return Ok(positions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching positions");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("GetPosition")]
        public async Task<IActionResult> GetPosition(string Id)
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
                var Position = await _positionService.GetPosition(Id);
                _logger.LogInformation("Test log", Position);
                return Ok(Position);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddPosition")]
        public async Task<IActionResult> AddPosition([FromBody] Position position)
        {
            if (position == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _positionService.AddPosition(position);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdatePosition")]
        public async Task<IActionResult> UpdatePosition([FromBody] Position position)
        {
            if (position == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _positionService.UpdatePosition(position);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("DeletePosition")]
        public async Task<IActionResult> DeletePosition(string id)
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
                var result = await _positionService.DeletePosition(id);
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
