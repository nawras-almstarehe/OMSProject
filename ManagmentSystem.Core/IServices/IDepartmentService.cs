using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.IServices
{
    public interface IDepartmentService
    {
        Task<(IEnumerable<VMDepartment> departments, int totalItems)> GetDepartmentsAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null);
        Task<IEnumerable<VMDepartmentsList>> GetDepartmentsList(string filter);
        Task<VMDepartment> GetDepartment(string Id);
        Task<int> AddDepartment(Department department);
        Task<int> UpdateDepartment(Department department);
        Task<int> DeleteDepartment(string Id);
    }
}
