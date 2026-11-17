using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.VModels
{
    public class VMResult
    {
        public int Code { get; set; }
        public bool Successed { get; set; }
        public string Message {  get; set; }
    }
}
