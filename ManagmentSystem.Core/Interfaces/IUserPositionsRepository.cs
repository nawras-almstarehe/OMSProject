using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Interfaces
{
    public interface IUserPositionsRepository : IBaseRepository<UserPosition>
    {
        public Task<double> GetUserPrivilegesAsync(string UserId);
    }
}
