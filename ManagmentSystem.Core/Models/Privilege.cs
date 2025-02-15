using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class Privilege
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public double Code { get; set; }
        public int Type { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public string EDescription { get; set; }
        public string ADescription { get; set; }
        public DateTime LastAccessed { get; set; }
        public ICollection<Role> Roles { get; set; }
        public List<RolePrivilege> RolePrivileges { get; set; }
    }
}
