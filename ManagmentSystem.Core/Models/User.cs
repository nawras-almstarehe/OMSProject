using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string UserName { get; set; }
        public string AFirstName { get; set; }
        public string EFirstName { get; set; }
        public string ALastName { get; set; }
        public string ELastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        //public bool IsEmployee { get; set; }
        //public bool IsProducer { get; set; }
        //public bool IsCustomer { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsAdmin { get; set; }
        public int BlockedType { get; set; }
        public int UserType { get; set; }
        public DateTime LastAccessed { get; set; }
        public ICollection<Position> Positions { get; set; }
        public List<UserPosition> UserPositions { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual UserProfile UserProfile { get; set; }

    }
}
