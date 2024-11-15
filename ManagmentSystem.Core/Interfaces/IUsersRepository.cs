using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Interfaces
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        Task<AuthModel> LoginAsync(VMLogin model);
        Task<VMResult> RegisterLocalAsync(VMRegister model);
        public Task<VMResult> CreateUserAsync(User user, string password);
        public Task<User> FindByEmailAsync(string Email);
        public Task<User> FindByUserNameAsync(string Username);
    }
}
