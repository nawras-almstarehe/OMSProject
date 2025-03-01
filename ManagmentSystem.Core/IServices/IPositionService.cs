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
    public interface IPositionService
    {
        Task<(IEnumerable<Position> positions, int totalItems)> GetPositionsAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null);
        Task<PositionDTO> GetPosition(string Id);
        Task<int> AddPosition(Position position);
        Task<int> UpdatePosition(Position position);
        Task<int> DeletePosition(string Id);
    }
}
