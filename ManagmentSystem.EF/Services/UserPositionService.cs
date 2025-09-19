using ManagmentSystem.Core.Dto;
using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.Resources;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Services
{
    public class UserPositionService : IUserPositionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IDistributedCache _cache;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public UserPositionService(IUnitOfWork unitOfWork, IWebHostEnvironment env, IDistributedCache cache, IStringLocalizer<SharedResource> localizer)
        {
            _unitOfWork = unitOfWork;
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _cache = cache;
            _localizer = localizer;
        }

        public async Task<int> AddAssignment(UserPosition userPosition)
        {
            try
            {
                var userPositionObj = new UserPosition { 
                    UserName = userPosition.UserName,
                    UserId = userPosition.UserId,
                    PositionId = userPosition.PositionId,
                    Type = (int)VMUserPosition.Enum_UserPosition_Type.Assigne,
                    IsActive = userPosition.IsActive,
                    StartDate = userPosition.StartDate,
                    EndDate = userPosition.EndDate,
                };
                var UserPosition = await _unitOfWork.UserPositions.Add(userPositionObj);
                if (UserPosition != null)
                {
                    await _unitOfWork.CompleteAsync();
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

        public async Task<int> DeleteAssignment(string Id)
        {
            try
            {
                var UserPosition = _unitOfWork.UserPositions.Delete(Id);
                if (UserPosition != 0)
                {
                    await _unitOfWork.CompleteAsync();
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

        //public async Task<VMUserPositionDepartment> GetUserPosition(string Id)
        //{
        //    try
        //    {
        //        var userPositionDto = await _unitOfWork.UserPositions.FindByAnyDataProjected<UserPosition, UserPositionDTO>(
        //                        p => p.Id == Id,
        //                        new[] { "User", "Position" },
        //                        up => new UserPositionDTO
        //                        {
        //                            Id = up.Id,
        //                            IsActive = up.IsActive,
        //                            Type = up.Type,
        //                            TypeName = "", // your helper method
        //                            AddedOn = up.AddedOn.ToString("yyyy-MM-dd"),
        //                            StartDate = up.StartDate.ToString("yyyy-MM-dd"),
        //                            EndDate = up.EndDate.ToString("yyyy-MM-dd"),
        //                            UserId = up.UserId,
        //                            UserName = up.User != null ? up.User.UserName : null,
        //                            EFullNameUser = up.User != null ? up.User.EFirstName + " " + up.User.ELastName : null,
        //                            AFullNameUser = up.User != null ? up.User.AFirstName + " " + up.User.ALastName : null,
        //                            PositionId = up.PositionId,
        //                            EPositionName = up.Position != null ? up.Position.EName : null,
        //                            APositionName = up.Position != null ? up.Position.AName : null
        //                        });
        //        var positionDto = await _unitOfWork.Positions.FindByAnyDataProjected<Position, PositionDTO>(
        //                        p => p.Id == userPositionDto.PositionId,
        //                        new[] { "Department" },
        //                        p => new PositionDTO
        //                        {
        //                            Id = p.Id,
        //                            AName = p.AName,
        //                            EName = p.EName,
        //                            IsActive = p.IsActive,
        //                            IsLeader = p.IsLeader,
        //                            DepartmentAName = p.Department.AName,
        //                            DepartmentEName = p.Department.EName,
        //                            DepartmentCode = p.Department.Code,
        //                            DepartmentId = p.Department.Id
        //                        });
        //        return new VMUserPositionDepartment(userPositionDto.Id, userPositionDto.UserName, userPositionDto.IsActive, userPositionDto.EPositionName,
        //            userPositionDto.APositionName, userPositionDto.TypeName, userPositionDto.EFullNameUser, userPositionDto.AFullNameUser, userPositionDto.AddedOn,
        //            userPositionDto.StartDate, userPositionDto.EndDate, userPositionDto.PositionId, userPositionDto.UserId, positionDto.DepartmentId,
        //            positionDto.DepartmentAName, positionDto.DepartmentEName, positionDto.DepartmentCode, userPositionDto.Type);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public async Task<VMUserPositionDepartment> GetUserPosition(string Id)
        {
            try
            {
                var result = await _unitOfWork.UserPositions.GetUserPositionDepartment(Id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(IEnumerable<VMUserPosition> userPositions, int totalItems)> GetUserPositionsAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null)
        {
            try
            {
                search ??= new Dictionary<string, string>();
                Expression<Func<UserPosition, bool>> match = u =>
                        (!search.ContainsKey("isActive") || string.IsNullOrEmpty(search["isActive"]) || u.IsActive == Convert.ToBoolean(search["isActive"]));

                int totalItems = await _unitOfWork.UserPositions.CountAsync(match);
                int countItems = await _unitOfWork.UserPositions.CountAllAsync();

                int skip = (page - 1) * pageSize;
                int take = pageSize;


                var allUserPositions = await _unitOfWork.UserPositions.FindByAnyDataProjectedList<UserPosition, UserPositionDTO>(
                                                projection: up => new UserPositionDTO
                                                {
                                                    Id = up.Id,
                                                    IsActive = up.IsActive,
                                                    Type = up.Type,
                                                    TypeName = "", // your helper method
                                                    AddedOn = up.AddedOn.ToString("yyyy-MM-dd"),
                                                    StartDate = up.StartDate.ToString("yyyy-MM-dd"),
                                                    EndDate = up.EndDate.ToString("yyyy-MM-dd"),
                                                    UserId = up.UserId,
                                                    UserName = up.User != null ? up.User.UserName : null,
                                                    EFullNameUser = up.User != null ? up.User.EFirstName + " " + up.User.ELastName: null,
                                                    AFullNameUser = up.User != null ? up.User.AFirstName + " " + up.User.ALastName : null,
                                                    PositionId = up.PositionId,
                                                    EPositionName = up.Position != null ? up.Position.EName : null,
                                                    APositionName = up.Position != null ? up.Position.AName : null
                                                },
                                                match: null,
                                                includes: new[] { "User", "Position" } // optional includes for navigation properties
                );

                var userPositions = await _unitOfWork.UserPositions.FindAllAsync(match, take, skip, sort);
                var UserPosResult = new List<VMUserPosition>();
                foreach (var UserPos in allUserPositions)
                {

                    UserPosResult.Add(new VMUserPosition(UserPos.Id, UserPos.UserName, UserPos.IsActive,
                        UserPos.EPositionName, UserPos.APositionName, GetTypeName(UserPos.Type), UserPos.EFullNameUser, UserPos.AFullNameUser,
                        UserPos.AddedOn, UserPos.StartDate, UserPos.EndDate));
                }
                return (UserPosResult, totalItems);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateAssignment(UserPosition userPosition)
        {
            try
            {
                var userPositionObj = new UserPosition
                {
                    Id = userPosition.Id,
                    UserName = userPosition.UserName,
                    UserId = userPosition.UserId,
                    PositionId = userPosition.PositionId,
                    Type = (int)VMUserPosition.Enum_UserPosition_Type.Assigne,
                    IsActive = userPosition.IsActive,
                    StartDate = userPosition.StartDate,
                    EndDate = userPosition.EndDate,
                };
                _unitOfWork.UserPositions.Update(userPositionObj);
                await _unitOfWork.CompleteAsync();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetTypeName(int type)
        {
            return type switch
            {
                (int)VMUserPosition.Enum_UserPosition_Type.HR => (string)_localizer["ResHR"],
                (int)VMUserPosition.Enum_UserPosition_Type.Assigne => (string)_localizer["ResAssign"],
                _ => "Unknown",
            };
        }

    }
}
