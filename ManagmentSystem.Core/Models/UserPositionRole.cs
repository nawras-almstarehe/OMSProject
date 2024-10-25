using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class UserPositionRole
    {
        public string Id { get; set; }
        public DateTime LastAccessed { get; set; }
        public DateTime AddedOn { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public string UserPositionId { get; set; }
        public UserPosition UserPosition { get; set; }
    }
}
