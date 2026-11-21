using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using ManagmentSystem.EF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagmentSystem.Controllers
{
    //[EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductesController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductesController> _logger;

        public ProductesController(IProductService productService, ILogger<ProductesController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        private string ResolveLang(string lang)
        {
            if (!string.IsNullOrWhiteSpace(lang))
                return lang.Trim().ToLowerInvariant();

            var headerLang = Request.Headers["Accept-Language"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(headerLang) && headerLang.ToLowerInvariant().StartsWith("en"))
                return "en";

            return "ar";
        }

        [HttpPost("GetProductes")]
        public async Task<IActionResult> GetProductes([FromBody] VMObjectPost objPost, [FromQuery] string lang = null)
        {
            objPost ??= new VMObjectPost();
            var resolvedLang = ResolveLang(lang);
            var (products, totalItems) = await _productService.GetProductsAll(
                objPost.Page,
                objPost.PageSize,
                resolvedLang,
                objPost.Filter,
                objPost.Sort);

            var data = VMResaultDataList<VMProduct>.SuccessResult(totalItems, products);
            return Ok(VMAllResultApi<VMResaultDataList<VMProduct>>.SuccessResult("Success", data));
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(string id, [FromQuery] string lang = null)
        {
            var resolvedLang = ResolveLang(lang);
            var product = await _productService.GetProduct(id, resolvedLang);
            if (product == null)
                return NotFound(VMAllResultApi<VMProduct>.FailureResult("Product not found"));

            return Ok(VMAllResultApi<VMProduct>.SuccessResult("Success", product));
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] VMProductPost model)
        {
            if (model == null)
                return BadRequest(VMAllResultApi<int>.FailureResult("Invalid payload"));

            try
            {
                var res = await _productService.AddProduct(model);
                return Ok(VMAllResultApi<int>.SuccessResult("Added successfully", res));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(VMAllResultApi<int>.FailureResult(ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(VMAllResultApi<int>.FailureResult(ex.Message));
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] VMProductPost model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Id))
                return BadRequest(VMAllResultApi<int>.FailureResult("Invalid payload"));

            try
            {
                var res = await _productService.UpdateProduct(model);
                if (res == 0)
                    return NotFound(VMAllResultApi<int>.FailureResult("Product not found"));

                return Ok(VMAllResultApi<int>.SuccessResult("Updated successfully", res));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(VMAllResultApi<int>.FailureResult(ex.Message));
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var res = await _productService.DeleteProduct(id);
            if (res == 0)
                return NotFound(VMAllResultApi<int>.FailureResult("Product not found"));

            return Ok(VMAllResultApi<int>.SuccessResult("Deleted successfully", res));
        }
    }
}
