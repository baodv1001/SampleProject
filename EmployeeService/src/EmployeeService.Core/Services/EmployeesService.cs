using EmployeeService.Core.Interfaces.Repositories;
using EmployeeService.Core.Interfaces.Services;
using EmployeeService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Services
{
    public class EmployeesService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }
        public async Task<bool> CreateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                throw new ArgumentNullException();
                return await _employeeRepository.GetAllEmployees();

            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
