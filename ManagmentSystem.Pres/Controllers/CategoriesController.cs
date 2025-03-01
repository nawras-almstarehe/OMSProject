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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpPost("GetCategories")]
        public async Task<IActionResult> GetCategories([FromBody] VMObjectPost CategoryPost)
        {
            if (CategoryPost == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var (categories, totalItems) = await _categoryService.GetCategoriesAll(CategoryPost.page, CategoryPost.pageSize, CategoryPost.filter, CategoryPost.sort);
                _logger.LogInformation("Test log", categories);
                return Ok(new VMResaultDataList<Category>(totalItems, categories));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory(string Id)
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
                var Category = await _categoryService.GetCategory(Id);
                _logger.LogInformation("Test log", Category);
                return Ok(Category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _categoryService.AddCategory(category);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            try
            {
                var result = await _categoryService.UpdateCategory(category);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(string id)
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
                var result = await _categoryService.DeleteCategory(id);
                _logger.LogInformation("Test log", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //[HttpGet]
        //public IActionResult GetById()
        //{
        //    return Ok(_unitOfWork.Categories.GetById(1));
        //}

        //[HttpGet("GetByName")]
        //public IActionResult GetByName()
        //{
        //    return Ok(_unitOfWork.Categories.Find(x => x.Name == "New Cat"));
        //}

        //[HttpGet("GetByAnyData")]// Get data from category with data of table another when category table relation to that table
        //public IActionResult GetByAnyData()
        //{
        //    return Ok(_unitOfWork.Categories.FindByAnyData(x => x.Name == "New Cat" , new[] { "ProductCategory"}));
        //}

        //[HttpGet("GetAllWithDataReleation")]// Get data from category with data of table another when category table relation to that table
        //public IActionResult GetAllWithDataReleation()
        //{
        //    return Ok(_unitOfWork.Categories.FindAll(x => x.Name == "New Cat", new[] { "ProductCategory" }));
        //}

        //[HttpGet("GetOrdered")]
        //public IActionResult GetOrdered()
        //{
        //    return Ok(_unitOfWork.Categories.FindAll(x => x.Name == "New Cat", null, null, y => y.Id, "DESC"));
        //}


        //[HttpGet("GetAllSpicialMethod")]
        //public IActionResult GetAllSpicialMethod()
        //{
        //    return Ok(_unitOfWork.Categories.GetAllSpecialMethod());//// After add spicial method now I can call to spicial method
        //}
    }
}
