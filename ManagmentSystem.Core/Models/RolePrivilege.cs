using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class RolePrivilege
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); public DateTime LastAccessed { get; set; }
        public DateTime AddedOn { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public string PrivilegeId { get; set; }
        public Privilege Privilege { get; set; }
    }
}
