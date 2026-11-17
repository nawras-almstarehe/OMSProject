using ManagmentSystem.Core.Dto;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.IServices
{
    public interface IRoleService
    {
        Task<(IEnumerable<Role> roles, int totalItems)> GetRolesAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null);
        Task<Role> GetRole(string Id);
        Task<int> AddRole(Role role);
        Task<int> UpdateRole(Role role);
        Task<int> DeleteRole(string Id);
    }
}
