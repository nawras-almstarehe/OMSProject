using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.VModels
{
    public class AuthModel
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string AFullName { get; set; }
        public string EFullName { get; set; }
        public string Email { get; set; }
        public double PriviligeCode { get; set; }
        public bool IsAuthenticated { get; set; }
        public int Language { get; set; }
        public int Theme { get; set; }
        public int Result { get; set; }
        public string Message { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
