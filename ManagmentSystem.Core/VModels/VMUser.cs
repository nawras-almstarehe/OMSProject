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
            None = 0
        }
        public enum Enum_User_Type
        {
            None = 0,
            Employee = 1,
            Producer = 2,
            Consumer = 3
        }
    }
}
