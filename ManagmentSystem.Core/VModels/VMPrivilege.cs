using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.VModels
{
    public class VMPrivilege
    {
        [Flags]
        public enum Enum_Privilege
        {
            None = 0,
            SystemManager = 1 << 0,
            Read = 1 << 1,
            Write = 1 << 2,
            Execute = 1 << 3,
            Delete = 1 << 4
        }
        public enum Enum_Privilege_Type
        {
            None = 0
        }
    }
}
