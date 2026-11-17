using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.Resources;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IDistributedCache _cache;
        public DepartmentService(IUnitOfWork unitOfWork, IWebHostEnvironment env, IStringLocalizer<SharedResource> localizer, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _localizer = localizer;
            _cache = cache;
        }
        public async Task<int> AddDepartment(Department department)
        {
            try
            {
                var DepCodeNew = await GenerateDepCode(department.DepartmentParentId) ?? throw new Exception("Error");
                var departmentObj = new Department
                {
                    EName = department.EName,
                    AName = department.AName,
                    Code = department.Code,
                    DepartmentParentId = department.DepartmentParentId,
                    DepartmentType = department.DepartmentType,
                    DepCode = DepCodeNew,
                    IsActive = department.IsActive,
                };
                var Department = await _unitOfWork.Departments.Add(departmentObj);
                if (Department != null)
                {
                    await _unitOfWork.CompleteAsync();
                    await _cache.RemoveAsync("AllDepartments");
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

        public async Task<int> DeleteDepartment(string Id)
        {
            try
            {
                var Department = _unitOfWork.Departments.Delete(Id);
                if (Department != 0)
                {
                    await _unitOfWork.CompleteAsync();
                    await _cache.RemoveAsync("AllDepartments");
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

        public async Task<VMDepartment> GetDepartment(string Id)
        {
            try
            {
                var Department = await _unitOfWork.Departments.GetByIdAsync(Id);
                var DepartmentTypeName = "";
                DepartmentTypeName = Department.DepartmentType switch
                {
                    (int)VMDepartment.Enum_Department_Type.GeneralDepartment => (string)_localizer["GeneralDepartment"],
                    _ => "",
                };
                var DepartmentParent = await _unitOfWork.Departments.GetByIdAsync(Department.DepartmentParentId);
                return new VMDepartment(Department.Id, Department.DepartmentParentId, Department.Code, Department.DepCode,
                    Department.AName, Department.EName, Department.IsActive, Department.DepartmentType,DepartmentTypeName, DepartmentParent);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(IEnumerable<VMDepartment> departments, int totalItems)> GetDepartmentsAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null)
        {
            try
            {
                search ??= new Dictionary<string, string>();
                Expression<Func<Department, bool>> match = u =>
                        (string.IsNullOrEmpty(search.GetValueOrDefault("aName")) || u.AName.Contains(search["aName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("eName")) || u.EName.Contains(search["eName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("code")) || u.Code.Contains(search["code"])) &&
                        (!search.ContainsKey("isActive") || string.IsNullOrEmpty(search["isActive"]) || u.IsActive == Convert.ToBoolean(search["isActive"]));

                int totalItems = await _unitOfWork.Departments.CountAsync(match);
                int countItems = await _unitOfWork.Departments.CountAllAsync();

                int skip = (page - 1) * pageSize;
                int take = pageSize;

                var departments = await _unitOfWork.Departments.FindAllAsync(match, take, skip, sort);
                var DepartmentsResult = new List<VMDepartment>();
                foreach (var Department in departments)
                {
                    var DepartmentTypeName = "";
                    DepartmentTypeName = Department.DepartmentType switch
                    {
                        (int)VMDepartment.Enum_Department_Type.GeneralDepartment => (string)_localizer["GeneralDepartment"],
                        _ => "",
                    };
                    DepartmentsResult.Add(new VMDepartment(Department.Id, Department.DepartmentParentId, Department.Code, Department.DepCode,
                    Department.AName, Department.EName, Department.IsActive, Department.DepartmentType, DepartmentTypeName));
                }
                return (DepartmentsResult, totalItems);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<VMDepartmentsList>> GetDepartmentsList(string filter)
        {
            try
            {
                string cacheKey = "AllDepartments";
                var cachedDepartments = await _cache.GetStringAsync(cacheKey);

                IEnumerable<VMDepartmentsList> allDepartments;

                if (!string.IsNullOrEmpty(cachedDepartments))
                {
                    // Deserialize cached data
                    allDepartments = JsonConvert.DeserializeObject<IEnumerable<VMDepartmentsList>>(cachedDepartments);
                }
                else
                {
                    // Fetch from database and cache it
                    allDepartments = await _unitOfWork.Departments.GetAllDepartmentsList(u => true);
                    var serializedDepartments = JsonConvert.SerializeObject(allDepartments);
                    await _cache.SetStringAsync(cacheKey, serializedDepartments, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                        SlidingExpiration = TimeSpan.FromMinutes(10)
                    });
                }

                // Apply filtering logic
                return string.IsNullOrEmpty(filter)
                    ? allDepartments
                    : allDepartments.Where(d =>
                        d.AName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                        d.EName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                        d.Code.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<int> UpdateDepartment(Department department)
        {
            try
            {
                var departmentOld = await _unitOfWork.Departments.GetByIdAsync(department.Id) ?? throw new Exception("Department not found.");
                var changeChild = false;
                var newDepCode = "";
                if (department.DepartmentParentId != departmentOld.DepartmentParentId)
                {
                    newDepCode = await GenerateDepCode(department.DepartmentParentId);
                    changeChild = true;
                }

                var departmentObj = new Department
                {
                    Id = department.Id,
                    EName = department.EName,
                    AName = department.AName,
                    Code = department.Code,
                    DepartmentParentId = department.DepartmentParentId,
                    DepartmentType = department.DepartmentType,
                    DepCode = newDepCode,
                    IsActive = department.IsActive,
                };
                _unitOfWork.Departments.Update(departmentObj);
                await _unitOfWork.CompleteAsync();

                if (changeChild)
                {
                    await UpdateChildDepartmentsDepCodes(department.Id);
                }
                await _cache.RemoveAsync("AllDepartments");
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Healper method
        private async Task<string> GenerateDepCode(string parentDepartmentId)
        {
            try
            {
                if (string.IsNullOrEmpty(parentDepartmentId))
                {
                    Expression<Func<Department, bool>> match1 = u => (string.IsNullOrEmpty(u.DepartmentParentId));
                    var maxRootCode = await _unitOfWork.Departments.GetMaxRootCode(match1);
                    if (maxRootCode == null || string.IsNullOrEmpty(maxRootCode))
                    {
                        return "0001";
                    }

                    int newRootNumber = int.Parse(maxRootCode) + 1;
                    return newRootNumber.ToString("D4");
                }

                Expression<Func<Department, bool>> match2 = u => (u.Id == parentDepartmentId);
                var parentDepartment = await _unitOfWork.Departments.FindAsync(match2);
                if (parentDepartment == null)
                {
                    return null;
                }

                string parentDepCode = parentDepartment.DepCode;

                Expression<Func<Department, bool>> match3 = u => (u.DepartmentParentId == parentDepartmentId);
                var lastChild = await _unitOfWork.Departments.GetLastChild(match3);

                string newChildCode;

                if (lastChild != null)
                {
                    int lastNumber = int.Parse(lastChild.DepCode[parentDepCode.Length..]);
                    newChildCode = $"{parentDepCode}{lastNumber + 1:D4}";
                }
                else
                {
                    newChildCode = $"{parentDepCode}0001";
                }

                return newChildCode;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateChildDepartmentsDepCodes(string parentId)
        {
            var children = await _unitOfWork.Departments.FindAllAsync(d => d.DepartmentParentId == parentId);
            foreach (var child in children)
            {
                var newDepCode = await GenerateDepCode(parentId);
                child.DepCode = newDepCode;

                _unitOfWork.Departments.Update(child);
                await _unitOfWork.CompleteAsync();
                try
                {
                    await UpdateChildDepartmentsDepCodes(child.Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating recursive hierarchy: {ex.Message}");
                }
            }
        }

        //private async Task<string> GenerateDepCode(string parentDepartmentId)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(parentDepartmentId))
        //        {
        //            Expression<Func<Department, bool>> match1 = u => (string.IsNullOrEmpty(u.DepartmentParentId));
        //            var maxRootCode = await _unitOfWork.Departments.GetMaxRootCode(match1);
        //            if (maxRootCode == null || string.IsNullOrEmpty(maxRootCode))
        //            {
        //                return "0001";
        //            }

        //            int newRootNumber = int.Parse(maxRootCode) + 1;
        //            return newRootNumber.ToString("D4");
        //        }

        //        Expression<Func<Department, bool>> match2 = u => (u.Id == parentDepartmentId);
        //        var parentDepartment = await _unitOfWork.Departments.FindAsync(match2);
        //        if (parentDepartment == null)
        //        {
        //            return null;
        //        }

        //        string parentDepCode = parentDepartment.DepCode;

        //        Expression<Func<Department, bool>> match3 = u => (u.DepartmentParentId == parentDepartmentId);
        //        var lastChild = await _unitOfWork.Departments.GetLastChild(match3);

        //        string newChildCode;

        //        if (lastChild != null)
        //        {
        //            int lastNumber = int.Parse(lastChild.DepCode[parentDepCode.Length..]);
        //            newChildCode = $"{parentDepCode}{lastNumber + 1:D4}";
        //        }
        //        else
        //        {
        //            newChildCode = $"{parentDepCode}0001";
        //        }

        //        return newChildCode;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public async Task UpdateDepartmentDepCode(Department department)
        //{
        //    var newDepCode = await GenerateDepCode(department.DepartmentParentId);
        //    department.DepCode = newDepCode;

        //    _unitOfWork.Departments.Update(department);

        //    await UpdateChildDepartmentsDepCodes(department.Id);
        //}

        //private async Task UpdateChildDepartmentsDepCodes(string parentId)
        //{
        //    var children = await _unitOfWork.Departments.FindAllAsync(d => d.DepartmentParentId == parentId);
        //    foreach (var child in children)
        //    {
        //        var updatedChildDepCode = await GenerateDepCode(parentId);
        //        if (child != null)
        //        {
        //            child.DepCode = updatedChildDepCode;
        //            _unitOfWork.Departments.Update(child);
        //            try
        //            {
        //                await UpdateChildDepartmentsDepCodes(child.Id);
        //            }
        //            catch (Exception ex)
        //            { 
        //                Console.WriteLine($"Error updating recursive hierarchy: {ex.Message}"); 
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Null reference encountered during recursive update.");
        //        }
        //    }
        //}
    }
}
