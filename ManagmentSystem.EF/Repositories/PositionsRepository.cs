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
    public class PositionsRepository : BaseRepository<Position>, IPositionsRepository
    {
        private new readonly ApplicationDBContext _context;
        public PositionsRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Position>> GetAllPositionsList(Expression<Func<Position, bool>> filter)
        {
            try
            {
                IQueryable<Position> query = _context.Set<Position>().Where(filter);
                var result = await query
                        .AsNoTracking()
                        .Select(d => new Position
                        {
                            Id = d.Id.ToString(),
                            EName = d.EName,
                            AName = d.AName,
                        })
                        .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                Console.WriteLine($"Error fetching departments: {ex.Message}");
                return Enumerable.Empty<Position>(); // Return an empty list on error
            }
        }
        public IEnumerable<Position> GetAllSpecialMethod()
        {
            throw new NotImplementedException();
        }
    }
}
