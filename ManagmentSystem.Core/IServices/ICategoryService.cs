using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.IServices
{
    public interface ICategoryService
    {
        Task<(IEnumerable<Category> categories, int totalItems)> GetCategoriesAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null);
        Task<Category> GetCategory(string Id);
        Task<int> AddCategory(Category category);
        Task<int> UpdateCategory(Category category);
        Task<int> DeleteCategory(string Id);
    }
}
