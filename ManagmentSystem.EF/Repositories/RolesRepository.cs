using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Repositories
{
    public class RolesRepository : BaseRepository<Role>, IRolesRepository
    {
        private new readonly ApplicationDBContext _context;
        public RolesRepository(ApplicationDBContext context) : base(context)
        {
        }
        public IEnumerable<Role> GetAllSpecialMethod()
        {
            throw new NotImplementedException();
        }
    }
}
