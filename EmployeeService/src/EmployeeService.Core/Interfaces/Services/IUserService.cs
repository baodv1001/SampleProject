using EmployeeService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetUserByUsername(string username);
    }
}
