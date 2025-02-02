using ManagmentSystem.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.IServices
{
    public interface IImageService
    {
        Task<IEnumerable<ImageFolder>> UploadFiles(string itemId, List<IFormFile> files);
        Task<IEnumerable<ImageFolder>> GetImagesByCategoryId(string categoryId);
        Task<int> DeleteEntity(string id);
    }
}
