using ManagmentSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Interfaces
{
    public interface IRolesRepository : IBaseRepository<Role>
    {
        IEnumerable<Role> GetAllSpecialMethod(); //Get all data but this special method for Category
    }
}
