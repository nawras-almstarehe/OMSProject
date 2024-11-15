using ManagmentSystem.Core.Interfaces;
using ManagmentSystem.Core.Models;
using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.Core.VModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.EF.Repositories
{
    public class UserPositionsRepository : BaseRepository<UserPosition>, IUserPositionsRepository
    {
        private new readonly ApplicationDBContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public UserPositionsRepository(ApplicationDBContext context) : base(context)
        {
        }
        public UserPositionsRepository(ApplicationDBContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserPosition> GetUserPositionByUserId(string UserId)
        {
            try
            {
                var userPosition = new UserPosition();
                if (string.IsNullOrEmpty(UserId))
                {
                    return null;
                }
                userPosition = await _unitOfWork.UserPositions.FindAsync(x => x.UserId == UserId);
                return userPosition;
            }
            catch (Exception)
            {
                throw;
            }
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
