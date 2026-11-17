using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Dto
{
    public class DepartmentDTO
    {
        public string Id { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public string Code { get; set; }
        public List<PositionDTO> Positions { get; set; }
    }
}
