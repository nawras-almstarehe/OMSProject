using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Repositories
{
    public class CategoriesRepository : BaseRepository<Category>, ICategoriesRepository
    {
        private new readonly ApplicationDBContext _context;
        public CategoriesRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetAllSpecialMethod()
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<VMCategoriesList>> GetAllCategoriesList(Expression<Func<Category, bool>> filter)
        {
            try
            {
                IQueryable<Category> query = _context.Set<Category>().Where(filter);
                var result = await query
                        .AsNoTracking()
                        .Select(d => new VMCategoriesList
                        {
                            id = d.Id.ToString(),
                            EName = d.EName,
                            AName = d.AName,
                        })
                        .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                Console.WriteLine($"Error fetching Categories: {ex.Message}");
                return Enumerable.Empty<VMCategoriesList>(); // Return an empty list on error
            }
        }
    }
}
