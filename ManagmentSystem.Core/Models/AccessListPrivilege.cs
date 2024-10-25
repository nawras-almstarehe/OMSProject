using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class AccessListPrivilege
    {
        public string Id { get; set; }
        public string UserPositionId { get; set; }
        public double Code { get; set; }
        public DateTime LastAccessed { get; set; }
        public virtual UserPosition UserPosition { get; set; }
    }
}
