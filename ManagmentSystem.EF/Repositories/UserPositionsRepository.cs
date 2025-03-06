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
