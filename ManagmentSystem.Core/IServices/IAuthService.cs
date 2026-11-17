using ManagmentSystem.Core.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.IServices
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(VMRegister model);
        Task<AuthModel> LoginAsync(VMLogin model);
    }
}
