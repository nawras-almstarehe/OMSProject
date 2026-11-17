using ManagmentSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.VModels
{
    public class VMUserPosition
    {
        public enum Enum_UserPosition_Type
        {
            HR = 1,
            Assigne = 2
        }

        public string Id { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public string AddedOn { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EFullNameUser { get; set; }
        public string AFullNameUser { get; set; }
        public string PositionId { get; set; }
        public string EPositionName { get; set; }
        public string APositionName { get; set; }

        public VMUserPosition(string Id, string UserName, bool IsActive, string EPositionName, string APositionName, string TypeName, string EFullNameUser, string AFullNameUser, string AddedOn,
            string StartDate, string EndDate)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.UserName = UserName;
            this.UserName = UserName;
            this.IsActive = IsActive;
            this.EPositionName = EPositionName;
            this.APositionName = APositionName;
            this.TypeName = TypeName;
            this.EFullNameUser = EFullNameUser;
            this.AFullNameUser = AFullNameUser;
            this.AddedOn = AddedOn;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
}
