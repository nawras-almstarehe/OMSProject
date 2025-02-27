using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using ManagmentSystem.EF.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace ManagmentSystem.Pres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentsController> _logger;
        public DepartmentsController(IDepartmentService departmentService, ILogger<DepartmentsController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [HttpPost("GetDepartments")]
        public async Task<IActionResult> GetDepartments([FromBody] VMObjectPost DepartmentPost)
        {
            if (DepartmentPost == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var (departments, totalItems) = await _departmentService.GetDepartmentsAll(DepartmentPost.page, DepartmentPost.pageSize, DepartmentPost.filter, DepartmentPost.sort);
                _logger.LogInformation("Test log", departments);
                return Ok(new VMResaultDataList<VMDepartment>(totalItems, departments));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetDepartmentsList")]
        public async Task<IActionResult> GetDepartmentsList(string filter)
        {
            try
            {
                var departments = await _departmentService.GetDepartmentsList(filter);
                _logger.LogInformation("Test log", departments);
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching departments");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("GetDepartment")]
        public async Task<IActionResult> GetDepartment(string Id)
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
                var Department = await _departmentService.GetDepartment(Id);
                _logger.LogInformation("Test log", Department);
                return Ok(Department);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _departmentService.AddDepartment(department);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _departmentService.UpdateDepartment(department);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(string id)
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
                var result = await _departmentService.DeleteDepartment(id);
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
