using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class RolePrivilege
    {
        public string Id { get; set; }
        public DateTime LastAccessed { get; set; }
        public DateTime AddedOn { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public string PrivilegeId { get; set; }
        public Privilege Privilege { get; set; }
    }
}
