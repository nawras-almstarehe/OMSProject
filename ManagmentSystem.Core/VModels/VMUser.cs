using ManagmentSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.VModels
{
    public class VMUser
    {
        public enum Enum_User_Blocked_Type
        {
            None = 0,
            ByAdmin = 1,
            BySystem = 2
        }
        public enum Enum_User_Type
        {
            None = 0,
            Employee = 1,
            Producer = 2,
            Consumer = 3
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string AFirstName { get; set; }
        public string EFirstName { get; set; }
        public string ALastName { get; set; }
        public string ELastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsAdmin { get; set; }
        public int BlockedType { get; set; }
        public int UserType { get; set; }
        public string UserTypeName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentEName { get; set; }
        public string DepartmentAName { get; set; }
        public string PositionId { get; set; }
        public string PositionAName { get; set; }
        public string PositionEName { get; set; }

        public VMUser(string Id, string UserName, string AFirstName, string EFirstName,
            string ALastName, string ELastName, string Email, string PhoneNumber,
            bool IsBlocked, bool IsAdmin, int BlockedType, int UserType, string UserTypeName, string DepartmentId
            , string DepartmentCode, string DepartmentEName, string DepartmentAName, string PositionId, string PositionAName, string PositionEName)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.AFirstName = AFirstName;
            this.EFirstName = EFirstName;
            this.ALastName = ALastName;
            this.ELastName = ELastName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.IsBlocked = IsBlocked;
            this.IsAdmin = IsAdmin;
            this.BlockedType = BlockedType;
            this.UserType = UserType;
            this.UserTypeName = UserTypeName;
            this.DepartmentId = DepartmentId;
            this.DepartmentCode = DepartmentCode;
            this.DepartmentEName = DepartmentEName;
            this.DepartmentAName = DepartmentAName;
            this.PositionId = PositionId;
            this.PositionAName = PositionAName;
            this.PositionEName = PositionEName;
        }
    }

    public class VMUserPost
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string AFirstName { get; set; }
        public string EFirstName { get; set; }
        public string ALastName { get; set; }
        public string ELastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsAdmin { get; set; }
        public int BlockedType { get; set; }
        public int UserType { get; set; }
        public string UserTypeName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentEName { get; set; }
        public string DepartmentAName { get; set; }
        public string PositionId { get; set; }
        public string PositionAName { get; set; }
        public string PositionEName { get; set; }
    }
}
