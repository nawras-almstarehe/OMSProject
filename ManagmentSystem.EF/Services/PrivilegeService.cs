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
    public class PrivilegeService : IPrivilegeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public PrivilegeService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task<int> AddPrivilege(Privilege privilege)
        {
            try
            {
                var privilegeObj = new Privilege { 
                    EName = privilege.EName,
                    AName = privilege.AName,
                    ADescription = privilege.ADescription,
                    EDescription = privilege.EDescription,
                    Type = privilege.Type,
                    Code = privilege.Code,
                };
                var Privilege = await _unitOfWork.Privileges.Add(privilegeObj);
                if (Privilege != null)
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

        public async Task<int> DeletePrivilege(string Id)
        {
            try
            {
                var Privilege = _unitOfWork.Privileges.Delete(Id);
                if (Privilege != 0)
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

        public async Task<(IEnumerable<Privilege> privileges, int totalItems)> GetPrivilegesAll(int page, int pageSize, Dictionary<string, string> search = null, ObjSort sort = null)
        {
            try
            {
                search ??= new Dictionary<string, string>();
                Expression<Func<Privilege, bool>> match = u =>
                        (string.IsNullOrEmpty(search.GetValueOrDefault("aName")) || u.AName.Contains(search["aName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("eName")) || u.EName.Contains(search["eName"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("eDescription")) || u.EDescription.Contains(search["eDescription"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("aDescription")) || u.ADescription.Contains(search["aDescription"])) &&
                        (string.IsNullOrEmpty(search.GetValueOrDefault("type")) || u.Type.Equals(search["type"]));

                int totalItems = await _unitOfWork.Privileges.CountAsync(match);
                int countItems = await _unitOfWork.Privileges.CountAllAsync();

                int skip = (page - 1) * pageSize;
                int take = pageSize;

                var privileges = await _unitOfWork.Privileges.FindAllAsync(match, take, skip, sort);
                return (privileges, totalItems);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Privilege> GetPrivilege(string Id)
        {
            try
            {
                var Privilege = await _unitOfWork.Privileges.GetByIdAsync(Id);
                return Privilege;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdatePrivilege(Privilege privilege)
        {
            try
            {
                var privilegeObj = new Privilege
                {
                    EName = privilege.EName,
                    AName = privilege.AName,
                    ADescription = privilege.ADescription,
                    EDescription = privilege.EDescription,
                    Type = privilege.Type,
                    Code = privilege.Code,
                };
                _unitOfWork.Privileges.Update(privilegeObj);
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
