using ManagmentSystem.Core.Dto;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.IServices
{
    public interface IUserPositionService
    {
        Task<(IEnumerable<VMUserPosition> userPositions, int totalItems)> GetUserPositionsAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null);
        Task<VMUserPositionDepartment> GetUserPosition(string Id);
        Task<int> AddAssignment(UserPosition UserPosition);
        Task<int> UpdateAssignment(UserPosition UserPosition);
        Task<int> DeleteAssignment(string Id);
    }
}
