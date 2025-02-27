using ManagmentSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.VModels
{
    public class VMDepartment
    {
        public enum Enum_Department_Type
        {
            None = 0,
            GeneralDepartment = 1
        }

        public string Id { get; set; }
        public string DepartmentParentId { get; set; }
        public Department DepartmentParent { get; set; }
        public string Code { get; set; }
        public string DepCode { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentType { get; set; }
        public string DepartmentTypeName { get; set; }
        public List<Position> positions { get; set; }

        public VMDepartment(string Id, string DepartmentParentId, string Code, string DepCode,
            string AName, string EName, bool IsActive, int DepartmentType,
            string DepartmentTypeName, Department departmentParent = null)
        {
            this.Id = Id;
            this.DepartmentParentId = DepartmentParentId;
            this.Code = Code;
            this.DepCode = DepCode;
            this.AName = AName;
            this.EName = EName;
            this.IsActive = IsActive;
            this.DepartmentType = DepartmentType;
            this.DepartmentTypeName = DepartmentTypeName;
            this.DepartmentParent = departmentParent;
        }
    }

    public class VMDepartmentsList
    {
        public string id { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
    }
}
