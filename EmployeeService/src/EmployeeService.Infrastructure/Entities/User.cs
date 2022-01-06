using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Entities
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }
        [ForeignKey("IdRole")]
        public Role Role { get; set; }

    }
}
