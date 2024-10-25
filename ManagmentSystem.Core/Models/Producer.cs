using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class Producer
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public DateTime LastAccessed { get; set; }
        public List<Process> Processes { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
