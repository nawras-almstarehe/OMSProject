using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Repositories
{
    public class PrivilegesRepository : BaseRepository<Privilege>, IPrivilegesRepository
    {
        private new readonly ApplicationDBContext _context;
        public PrivilegesRepository(ApplicationDBContext context) : base(context)
        {
        }
        public IEnumerable<Privilege> GetAllSpecialMethod()
        {
            throw new NotImplementedException();
        }
    }
}
