using EmployeeService.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Services
{
    public class HeavyHorn : IHorn
    {
        public string Beep()
        {
            return "Beep to";
        }
    }
}
