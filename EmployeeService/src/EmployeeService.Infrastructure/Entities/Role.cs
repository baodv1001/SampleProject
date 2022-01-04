using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Entities
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string Name { get; set; }
        /*public ICollection<User> Users { get; set;}*/
    }
}
