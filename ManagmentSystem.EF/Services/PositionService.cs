using ManagmentSystem.Core.Dto;
using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Services
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IDistributedCache _cache;
        public PositionService(IUnitOfWork unitOfWork, IWebHostEnvironment env, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _cache = cache; 
        }

        public async Task<int> AddPosition(Position position)
        {
            try
            {
                var positionObj = new Position { EName = position.EName, AName = position.AName, DepartmentId = position.DepartmentId };
                var Position = await _unitOfWork.Positions.Add(positionObj);
                if (Position != null)
                {
                    await _unitOfWork.CompleteAsync();
                    await _cache.RemoveAsync("AllPositions");
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeletePosition(string Id)
        {
            try
            {
                var Position = _unitOfWork.Positions.Delete(Id);
                if (Position != 0)
                {
                    await _unitOfWork.CompleteAsync();
                    await _cache.RemoveAsync("AllPositions");
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(IEnumerable<Position> positions, int totalItems)> GetPositionsAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null)
        {
            try
            {
                search ??= new Dictionary<string, string>();
                Expression<Func<Position, bool>> match = u =>
                        (string.IsNullOrEmpty(search.GetValueOrDefault("aName")) || u.AName.Contains(search["aName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("eName")) || u.EName.Contains(search["eName"])) &&
                        (!search.ContainsKey("isActive") || string.IsNullOrEmpty(search["isActive"]) || u.IsActive == Convert.ToBoolean(search["isActive"])) &&
                        (!search.ContainsKey("isLeader") || string.IsNullOrEmpty(search["isLeader"]) || u.IsLeader == Convert.ToBoolean(search["isLeader"]));

                int totalItems = await _unitOfWork.Positions.CountAsync(match);
                int countItems = await _unitOfWork.Positions.CountAllAsync();

                int skip = (page - 1) * pageSize;
                int take = pageSize;

                var positions = await _unitOfWork.Positions.FindAllAsync(match, take, skip, sort);
                return (positions, totalItems);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Position>> GetPositionsList(string departmentId, string filter)
        {
            try
            {
                string cacheKey = "AllPositions";
                var cachedData = await _cache.GetStringAsync(cacheKey);

                IEnumerable<Position> allPositions;

                if (!string.IsNullOrEmpty(cachedData))
                {
                    // Deserialize cached data
                    allPositions = JsonConvert.DeserializeObject<IEnumerable<Position>>(cachedData);
                }
                else
                {
                    // Fetch from database and cache it
                    allPositions = await _unitOfWork.Positions.GetAllAsync();
                    var serializedData = JsonConvert.SerializeObject(allPositions);
                    await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                        SlidingExpiration = TimeSpan.FromMinutes(10)
                    });
                }

                // Apply filtering logic
                return string.IsNullOrEmpty(filter)
                    ? allPositions.Where(p => p.DepartmentId.Equals(departmentId, StringComparison.OrdinalIgnoreCase))
                    : allPositions.Where(p =>
                        p.DepartmentId.Equals(departmentId, StringComparison.OrdinalIgnoreCase) &&
                        (p.AName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                         p.EName.Contains(filter, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<PositionDTO> GetPosition(string Id)
        {
            try
            {
                var positionDto = await _unitOfWork.Positions.FindByAnyDataProjected<Position, PositionDTO>(
                                p => p.Id == Id,
                                new[] { "Department" },
                                p => new PositionDTO
                                {
                                    Id = p.Id,
                                    AName = p.AName,
                                    EName = p.EName,
                                    IsActive = p.IsActive,
                                    IsLeader = p.IsLeader,
                                    DepartmentAName = p.Department.AName,
                                    DepartmentEName = p.Department.EName,
                                    DepartmentCode = p.Department.Code,
                                    DepartmentId = p.Department.Id
                                });
                return positionDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdatePosition(Position position)
        {
            try
            {
                var positionObj = new Position { Id = position.Id, EName = position.EName, AName = position.AName, DepartmentId = position.DepartmentId };
                _unitOfWork.Positions.Update(positionObj);
                await _unitOfWork.CompleteAsync();
                await _cache.RemoveAsync("AllPositions");
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
