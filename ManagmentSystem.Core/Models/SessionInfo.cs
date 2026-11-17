using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class SessionInfo
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); public string SessionId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }
        public int Language { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime LastAccessed { get; set; }
    }
}
