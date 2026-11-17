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
    public interface IPrivilegeService
    {
        Task<(IEnumerable<Privilege> privileges, int totalItems)> GetPrivilegesAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null);
        Task<Privilege> GetPrivilege(string Id);
        Task<int> AddPrivilege(Privilege privilege);
        Task<int> UpdatePrivilege(Privilege privilege);
        Task<int> DeletePrivilege(string Id);
    }
}
