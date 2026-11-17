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
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public ObjSort sort { get; set; }
        public Dictionary<string, string> filter { get; set; }
    }

    public class ObjSort
    {
        public string column { get; set; }
        public string state { get; set; }
    }
}
