using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
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
    public class PrivilegesController : ControllerBase
    {
        private readonly IPrivilegeService _privilegeService;
        private readonly ILogger<PrivilegesController> _logger;
        public PrivilegesController(IPrivilegeService privilegeService, ILogger<PrivilegesController> logger)
        {
            _privilegeService = privilegeService;
            _logger = logger;
        }

        [HttpPost("GetPrivileges")]
        public async Task<IActionResult> GetPrivileges([FromBody] VMObjectPost PrivilegePost)
        {
            if (PrivilegePost == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var (privileges, totalItems) = await _privilegeService.GetPrivilegesAll(PrivilegePost.page, PrivilegePost.pageSize, PrivilegePost.filter, PrivilegePost.sort);
                _logger.LogInformation("Test log", privileges);
                return Ok(new VMResaultDataList<Privilege>(totalItems, privileges));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetPrivilege")]
        public async Task<IActionResult> GetPrivilege(string Id)
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
                var Privilege = await _privilegeService.GetPrivilege(Id);
                _logger.LogInformation("Test log", Privilege);
                return Ok(Privilege);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddPrivilege")]
        public async Task<IActionResult> AddPrivilege([FromBody] Privilege privilege)
        {
            if (privilege == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _privilegeService.AddPrivilege(privilege);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdatePrivilege")]
        public async Task<IActionResult> UpdatePrivilege([FromBody] Privilege privilege)
        {
            if (privilege == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _privilegeService.UpdatePrivilege(privilege);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("DeletePrivilege")]
        public async Task<IActionResult> DeletePrivilege(string id)
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
                var result = await _privilegeService.DeletePrivilege(id);
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
