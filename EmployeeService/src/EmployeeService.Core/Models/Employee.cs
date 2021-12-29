using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
        public string Level { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
