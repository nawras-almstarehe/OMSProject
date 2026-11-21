using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Interfaces
{
    public interface ICategoriesRepository : IBaseRepository<Category>
    {
        IEnumerable<Category> GetAllSpecialMethod(); //Get all data but this special method for Category
        Task<IEnumerable<VMCategoriesList>> GetAllCategoriesList(Expression<Func<Category, bool>> filter);
    }
}
