using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class Privilege
    {
        public string Id { get; set; }
        public double Code { get; set; }
        public string Type { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public string EDescription { get; set; }
        public string ADescription { get; set; }
        public DateTime LastAccessed { get; set; }
        public ICollection<Role> Roles { get; set; }
        public List<RolePrivilege> RolePrivileges { get; set; }
    }
}
