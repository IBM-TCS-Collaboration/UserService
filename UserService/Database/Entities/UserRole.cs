using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Database.Entities
{
    public class UserRole
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        
    }
}
