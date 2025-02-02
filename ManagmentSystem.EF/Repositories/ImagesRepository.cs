using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Repositories
{
    public class ImagesRepository : BaseRepository<ImageFolder>, IImagesRepository
    {
        private new readonly ApplicationDBContext _context;
        public ImagesRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ImageFolder>> GetImagesByCategoryId(string categoryId)
        {
            IQueryable<ImageFolder> query = _context.Set<ImageFolder>();
            return await query.Where(img => img.CategoryId == categoryId).ToListAsync();
        }
    }
}
