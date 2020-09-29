using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Database.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Mobileno { get; set; }

        public string EmailID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }

        public DateTime Birthdate { get; set; }
        public int RoleID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EmployeeID { get; set; }
        public DateTime DateofJoining { get; set; }
        public int ForceChangePassword { get; set; }
    }

    
}
