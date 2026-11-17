using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Interfaces
{
    public interface IDepartmentsRepository : IBaseRepository<Department>
    {
        Task<IEnumerable<VMDepartmentsList>> GetAllDepartmentsList(Expression<Func<Department, bool>> filter);
        Task<string> GetMaxRootCode(Expression<Func<Department, bool>> parentDepartmentId);
        Task<Department> GetLastChild(Expression<Func<Department, bool>> parentDepartmentId);
    }
}
