using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Dto
{
    public class PositionDTO
    {
        public string Id { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public bool IsActive { get; set; }
        public bool IsLeader { get; set; }
        public string DepartmentEName { get; set; } // Flattened property
        public string DepartmentAName { get; set; } // Flattened property
        public string DepartmentCode { get; set; } // Flattened property
        public string DepartmentId { get; set; }
    }
}
