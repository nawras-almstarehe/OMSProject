using ManagmentSystem.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ManagmentSystem.Core.IServices;
using Microsoft.Extensions.Logging;

namespace ManagmentSystem.Pres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ILogger<UsersController> _logger;

        public ImagesController(IImageService imageService, ILogger<UsersController> logger)
        {
            _imageService = imageService;
            _logger =logger;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] string itemId, [FromForm] List<IFormFile> files)
        {
            if (itemId == null || itemId == "" || files.Count == 0)
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var Images = await _imageService.UploadFiles(itemId, files);
                _logger.LogInformation("Test log", Images);
                return Ok(Images);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetImagesByCategoryId")]
        public async Task<IActionResult> GetImagesByCategoryId(string categoryId)
        {
            if (categoryId == null || categoryId == "")
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var Images = await _imageService.GetImagesByCategoryId(categoryId);
                _logger.LogInformation("Test log", Images);
                return Ok(Images);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string imageId)
        {
            if (imageId == null || imageId == "")
            {
                return BadRequest(new
                {
                    Message = "Invalid request payload.",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                int result = await _imageService.DeleteEntity(imageId);
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
