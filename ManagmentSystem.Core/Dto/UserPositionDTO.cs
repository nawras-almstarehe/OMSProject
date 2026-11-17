using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Dto
{
    public class UserPositionDTO
    {
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
    }
}
