using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.VModels
{
    public class VMAllResultApi<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public VMAllResultApi() { }

        public VMAllResultApi(bool success, string message, T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static VMAllResultApi<T> SuccessResult(string message, T data)
        {
            return new VMAllResultApi<T>(true, message, data);
        }

        public static VMAllResultApi<T> FailureResult(string message)
        {
            return new VMAllResultApi<T>(false, message);
        }
    }
}
