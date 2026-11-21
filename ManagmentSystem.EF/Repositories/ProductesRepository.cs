using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Repositories
{
    public class ProductesRepository : BaseRepository<Product>, IProductesRepository
    {
        private new readonly ApplicationDBContext _context;
        public ProductesRepository(ApplicationDBContext context) : base(context)
        {
        }
        public IEnumerable<Product> GetAllSpecialMethod()
        {
            throw new NotImplementedException();
        }
    }
}
