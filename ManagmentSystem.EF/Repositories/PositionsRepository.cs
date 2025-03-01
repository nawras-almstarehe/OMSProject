using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Repositories
{
    public class PositionsRepository : BaseRepository<Position>, IPositionsRepository
    {
        private new readonly ApplicationDBContext _context;
        public PositionsRepository(ApplicationDBContext context) : base(context)
        {
        }
        public IEnumerable<Position> GetAllSpecialMethod()
        {
            throw new NotImplementedException();
        }
    }
}
