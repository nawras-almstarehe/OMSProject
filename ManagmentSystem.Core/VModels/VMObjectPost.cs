using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.VModels
{
    public class VMObjectPost
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public ObjSort Sort { get; set; }
        public Dictionary<string, string> Filter { get; set; }
    }

    public class ObjSort
    {
        public string column { get; set; }
        public string state { get; set; }
    }
}
