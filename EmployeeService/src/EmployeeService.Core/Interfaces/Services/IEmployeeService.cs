using EmployeeService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<bool> CreateEmployee(Employee employee);
        Task<bool> UpdateEmployee(Employee employee);
        Task DeleteEmployee(int id);
    }
}
