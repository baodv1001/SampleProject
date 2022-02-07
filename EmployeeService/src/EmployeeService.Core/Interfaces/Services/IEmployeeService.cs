using EmployeeService.Core.Helpers;
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
        Task<PagedList<Employee>> GetAllEmployees(EmployeeParameters employeeParameters);
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> CreateEmployee(Employee employee);
        Task<Object> UpdateEmployee(Employee employee, int id);
        Task<bool> DeleteEmployee(int id);
    }
}
