using ManagmentSystem.Core.IServices;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public RoleService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task<int> AddRole(Role role)
        {
            try
            {
                var roleObj = new Role
                {
                    EName = role.EName,
                    AName = role.AName
                };
                var Role = await _unitOfWork.Roles.Add(roleObj);
                if (Role != null)
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

        public async Task<int> DeleteRole(string Id)
        {
            try
            {
                var Role = _unitOfWork.Roles.Delete(Id);
                if (Role != 0)
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

        public async Task<(IEnumerable<Role> roles, int totalItems)> GetRolesAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null)
        {
            try
            {
                search ??= new Dictionary<string, string>();
                Expression<Func<Role, bool>> match = u =>
                        (string.IsNullOrEmpty(search.GetValueOrDefault("aName")) || u.AName.Contains(search["aName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("eName")) || u.EName.Contains(search["eName"]));

                int totalItems = await _unitOfWork.Roles.CountAsync(match);
                int countItems = await _unitOfWork.Roles.CountAllAsync();

                int skip = (page - 1) * pageSize;
                int take = pageSize;

                var roles = await _unitOfWork.Roles.FindAllAsync(match, take, skip, sort);
                return (roles, totalItems);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Role> GetRole(string Id)
        {
            try
            {
                var Role = await _unitOfWork.Roles.GetByIdAsync(Id);
                return Role;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateRole(Role role)
        {
            try
            {
                var roleObj = new Role
                {
                    EName = role.EName,
                    AName = role.AName
                };
                _unitOfWork.Roles.Update(roleObj);
                await _unitOfWork.CompleteAsync();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
