using ManagmentSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagmentSystem.Core.Interfaces
{
    public interface ICategoryProductesRepository : IBaseRepository<CategoryProduct>
    {
        IEnumerable<CategoryProduct> GetAllSpecialMethod(); //Get all data but this special method for Category
    }
}
