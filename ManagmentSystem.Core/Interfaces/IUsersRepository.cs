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
    public interface IUsersRepository : IBaseRepository<User>
    {
        Task<VMResult> RegisterLocalAsync(VMRegister model);
        Task<VMResult> CreateUserAsync(User user, string password);
        Task<User> FindByEmailAsync(string Email);
        Task<User> FindByUserNameAsync(string Username);
        Task<IEnumerable<VMUsersList>> GetAllUsersList(Expression<Func<User, bool>> filter);
    }
}
