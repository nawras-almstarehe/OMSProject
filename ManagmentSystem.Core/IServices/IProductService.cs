using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ManagmentSystem.Core.VModels.VMUserProfile;

namespace ManagmentSystem.Core.IServices
{
    public interface IProductService
    {
        Task<(IEnumerable<VMProduct> products, int totalItems)> GetProductsAll(int page,
            int pageSize,
            string lang = "ar",
            Dictionary<string, string> search = null,
            ObjSort sort = null
        );
        Task<VMProduct> GetProduct(string id,
            string lang = "ar");
        Task<int> AddProduct(VMProductPost product);
        Task<int> UpdateProduct(VMProductPost product);
        Task<int> DeleteProduct(string id);
    }
}