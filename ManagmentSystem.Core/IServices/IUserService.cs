using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.IServices
{
    public interface IUserService
    {
        Task<(IEnumerable<VMUser> users, int totalItems)> GetUsersAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null);
        Task<IEnumerable<VMUsersList>> GetUsersList(string filter);
        Task<VMUser> GetUser(string Id);
        Task<AuthModel> AddUser(VMUserPost user);
        Task<AuthModel> UpdateUser(VMUserPost user);
        Task<int> DeleteUser(string Id);
        Task<bool> CheckExistEmailForUpdate(string Email, string id);
        Task<bool> CheckExistUserNameForUpdate(string UserName, string id);
    }
}
