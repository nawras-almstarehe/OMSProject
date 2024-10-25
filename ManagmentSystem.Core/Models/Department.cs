﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class Department
    {
        public string Id { get; set; }
        public string DepartmentParentId { get; set; }
        public string DepartmentType { get; set; }
        public string Code { get; set; }
        public string DepCode { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastAccessed { get; set; }
        public List<Position> Positions { get; set; }
    }
}
