using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class Department
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string DepartmentParentId { get; set; }
        public int DepartmentType { get; set; }
        public Department DepartmentParent { get; set; }
        public string Code { get; set; }
        public string DepCode { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastAccessed { get; set; }
        public List<Position> Positions { get; set; }
    }
}
