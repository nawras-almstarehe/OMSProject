using ManagmentSystem.Core.Models;
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
            None = '0',
            ByAdmin = '1',
            BySystem = '2'
        }
        public enum Enum_User_Type
        {
            None = '0',
            Employee = '1',
            Producer = '2',
            Consumer = '3'
        }

        public string id { get; set; }
        public string userName { get; set; }
        public string aFirstName { get; set; }
        public string eFirstName { get; set; }
        public string aLastName { get; set; }
        public string eLastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public bool isBlocked { get; set; }
        public bool isAdmin { get; set; }
        public string blockedType { get; set; }
        public string userType { get; set; }
        public List<Position> positions { get; set; }
        public List<UserPosition> userPositions { get; set; }
        public Customer customer { get; set; }
        public Producer producer { get; set; }
        public UserProfile userProfile { get; set; }

        public VMUser(string Id, string UserName, string AFirstName, string EFirstName,
            string ALastName, string ELastName, string Email, string PhoneNumber,
            bool IsBlocked, bool IsAdmin, string BlockedType, string UserType)
        {
            id = Id;
            userName = UserName;
            aFirstName = AFirstName;
            eFirstName = EFirstName;
            aLastName = ALastName;
            eLastName = ELastName;
            email = Email;
            phoneNumber = PhoneNumber;
            isBlocked = IsBlocked;
            isAdmin = IsAdmin;
            blockedType = BlockedType;
            userType = UserType;
        }
    }
}
