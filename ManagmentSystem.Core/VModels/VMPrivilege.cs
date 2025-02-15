using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.VModels
{
    public class VMPrivilege
    {
        public enum Enum_Privilege
        {
            None = 0,
            SystemManager = 1,
            Read = 2,
            Write = 4,
            Execute = 8,
            Delete = 16
        }
        public enum Enum_Privilege_Type
        {
            None = 0
        }
    }
}
