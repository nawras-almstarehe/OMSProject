using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class Position
    {
        public string Id { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public bool IsLeader { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastAccessed { get; set; }
        public ICollection<User> Users { get; set; }
        public List<UserPosition> UserPositions { get; set; }
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
