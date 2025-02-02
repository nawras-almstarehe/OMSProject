using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class UserProfile
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); public string UserId { get; set; }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public int Language { get; set; }
        public int Theme { get; set; }
        public virtual User User { get; set; }
        public DateTime LastAccessed { get; set; }
    }
}
