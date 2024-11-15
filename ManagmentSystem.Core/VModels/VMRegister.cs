using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.VModels
{
    public class VMRegister
    {
        [Required, StringLength(100)]
        public string AFirstName { get; set; }

        [Required, StringLength(100)]
        public string EFirstName { get; set; }

        [Required, StringLength(100)]
        public string ALastName { get; set; }

        [Required, StringLength(100)]
        public string ELastName { get; set; }

        [Required, StringLength(100)]
        public string AName { get; set; }

        [Required, StringLength(100)]
        public string EName { get; set; }

        [Required, StringLength(100)]
        public string Address { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsAdmin { get; set; }
        public string PhoneNumber { get; set; }
    }
}
