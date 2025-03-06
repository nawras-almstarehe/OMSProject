using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ManagmentSystem.EF.Repositories
{
    public class DepartmentsRepository : BaseRepository<Department>, IDepartmentsRepository
    {
        private new readonly ApplicationDBContext _context;
        public DepartmentsRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VMDepartmentsList>> GetAllDepartmentsList(Expression<Func<Department, bool>> filter)
        {
            try
            {
                IQueryable<Department> query = _context.Set<Department>().Where(filter);
                var result = await query
                        .AsNoTracking()
                        .Select(d => new VMDepartmentsList
                        {
                            id = d.Id.ToString(),
                            EName = d.EName,
                            AName = d.AName,
                            Code = d.Code,
                        })
                        .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                Console.WriteLine($"Error fetching departments: {ex.Message}");
                return Enumerable.Empty<VMDepartmentsList>(); // Return an empty list on error
            }
        }

        public async Task<Department> GetLastChild(Expression<Func<Department, bool>> parentDepartmentId)
        {
            try
            {
                IQueryable<Department> query = _context.Set<Department>().Where(parentDepartmentId);
                var lastChild = await query
                    .OrderByDescending(d => d.DepCode)
                    .FirstOrDefaultAsync();
                return lastChild;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching departments: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetMaxRootCode(Expression<Func<Department, bool>> parentDepartmentId)
        {
            try
            {
                IQueryable<Department> query = _context.Set<Department>().Where(parentDepartmentId);
                var result = await query
                        .Select(d => d.DepCode)
                        .OrderByDescending(c => c)
                        .FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching departments: {ex.Message}");
                return "";
            }
        }
    }
}
