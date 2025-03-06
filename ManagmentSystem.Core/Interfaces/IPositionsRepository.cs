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
    public interface IPositionsRepository : IBaseRepository<Position>
    {
        Task<IEnumerable<Position>> GetAllPositionsList(Expression<Func<Position, bool>> filter);
        IEnumerable<Position> GetAllSpecialMethod(); //Get all data but this special method for Category
    }
}
