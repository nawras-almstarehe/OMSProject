using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public ImageService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env ?? throw new ArgumentNullException(nameof(env)); ;
        }

        public async Task<int> DeleteEntity(string id)
        {
            var image =  _unitOfWork.Images.GetById(id);
            if (image != null)
            {
                var filePath = Path.Combine(_env.WebRootPath, image.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _unitOfWork.Images.DeleteEntity(image);
                return await _unitOfWork.CompleteAsync();
            }
            return -1;
        }

        public async Task<IEnumerable<ImageFolder>> GetImagesByCategoryId(string categoryId)
        {
            try
            {
                var Images = await _unitOfWork.Images.GetImagesByCategoryId(categoryId);
                return Images;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ImageFolder>> UploadFiles(string itemId, List<IFormFile> files)
        {
            var imageList = new List<ImageFolder>();
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (var file in files)
            {
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var imageUrl = $"/uploads/{fileName}";
                var image = new ImageFolder { CategoryId = itemId, ImagePath = imageUrl };

                try
                {
                    await _unitOfWork.Images.Add(image);
                    var result = await _unitOfWork.CompleteAsync(); // Ensure commit works

                    if (result == 0)
                    {
                        throw new Exception("Database save failed! No records were affected.");
                    }

                    imageList.Add(image);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database Save Error: {ex.Message}");
                }
            }

            return imageList;
        }
    }
}
