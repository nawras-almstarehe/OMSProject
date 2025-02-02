using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class UserPosition
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); public bool IsActive { get; set; }
        public string Type { get; set; }
        public DateTime LastAccessed { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public User User { get; set; }
        public string PositionId { get; set; }
        public Position Position { get; set; }
        public ICollection<Role> Roles { get; set; }
        public List<UserPositionRole> UserPositionRoles { get; set; }
        public virtual AccessListPrivilege AccessListPrivilege { get; set; }
    }
}
