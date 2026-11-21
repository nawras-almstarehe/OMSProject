using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagmentSystem.EF.Repositories
{
    public class CategoryProductesRepository : BaseRepository<CategoryProduct>, ICategoryProductesRepository
    {
        private new readonly ApplicationDBContext _context;
        public CategoryProductesRepository(ApplicationDBContext context) : base(context)
        {
        }
        public IEnumerable<CategoryProduct> GetAllSpecialMethod()
        {
            throw new NotImplementedException();
        }
    }
}
