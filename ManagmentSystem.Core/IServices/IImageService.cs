using ManagmentSystem.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.IServices
{
    public interface IImageService
    {
        Task<IEnumerable<ImageFolder>> UploadFiles(string itemId, List<IFormFile> files,bool fromProduct);
        Task<IEnumerable<ImageFolder>> GetImagesByCategoryId(string categoryId);
        Task<IEnumerable<ImageFolder>> GetImagesByProductId(string productId);
        Task<int> DeleteEntity(string id);
    }
}
