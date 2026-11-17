using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.VModels
{
    public class VMResaultDataList<T>
    {
        public int totalItems { get; set; }
        public IEnumerable<T> Data { get; set; }

        public VMResaultDataList() { }

        public VMResaultDataList(int total, IEnumerable<T> data = default)
        {
            totalItems = total;
            Data = data;
        }

        public static VMResaultDataList<T> SuccessResult(int total, IEnumerable<T> data)
        {
            return new VMResaultDataList<T>(total, data);
        }

        public static VMResaultDataList<T> FailureResult()
        {
            return new VMResaultDataList<T>(0);
        }
    }
}
