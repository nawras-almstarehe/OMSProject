using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class Role
    {
        public string Id { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public DateTime LastAccessed { get; set; }
        public ICollection<Privilege> Privileges { get; set; }
        public ICollection<UserPosition> UserPositiones { get; set; }
        public List<RolePrivilege> RolePrivileges { get; set; }
        public List<UserPositionRole> UserPositionRoles { get; set; }
    }
}
