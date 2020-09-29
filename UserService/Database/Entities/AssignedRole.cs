using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Database.Entities
{
    public class AssignedRole
    {
        [Key]
        public int AssignedRolesID { get; set; }
        public int AssignToAdmin { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int UserID { get; set; }
    }
}
