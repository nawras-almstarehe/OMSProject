using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Repositories
{
    public class UserPositionsRepository : BaseRepository<UserPosition>, IUserPositionsRepository
    {
        private new readonly ApplicationDBContext _context;
        public UserPositionsRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<VMUserPositionDepartment> GetUserPositionDepartment(string Id)
        {
            var query = from up in _context.UsersPositions
                        join p in _context.Positiones on up.PositionId equals p.Id
                        join d in _context.Departmentes on p.DepartmentId equals d.Id
                        join u in _context.Users on up.UserId equals u.Id
                        where up.Id == Id
                        select new VMUserPositionDepartment
                        (
                            up.Id,
                            u.UserName,
                            up.IsActive,
                            p.EName,
                            p.AName,
                            "", // دالة ترجمة النوع هنا
                            u.EFirstName + " " + u.ELastName,
                            u.AFirstName + " " + u.ALastName,
                            up.AddedOn.ToString("yyyy-MM-dd"),
                            up.StartDate.ToString("yyyy-MM-dd"),
                            up.EndDate.ToString("yyyy-MM-dd"),
                            p.Id,
                            up.UserId,
                            d.Id,
                            d.AName,
                            d.EName,
                            d.Code,
                            up.Type
                        );

            var result = await ExecuteQueryProjected(query);

            return result;
        }

        public async Task<double> GetUserPrivilegesAsync(string UserId)
        {
            try
            {
                double privilegeCode = (double)(VMPrivilege.Enum_Privilege.None);
                if (string.IsNullOrEmpty(UserId))
                {
                    return privilegeCode;
                }
                privilegeCode = await (from alp in _context.AccessListPrivileges
                              join up in _context.UsersPositions on alp.UserPositionId equals up.Id
                              join u in _context.Users on up.UserId equals u.Id
                              where u.Id == UserId
                              select (double)(alp.Code)).FirstOrDefaultAsync();

                return privilegeCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
