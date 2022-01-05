using EmployeeService.Core.Interfaces.Repositories;
using EmployeeService.Core.Interfaces.Services;
using EmployeeService.Core.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EmployeesService> _logger;

        public EmployeesService(IEmployeeRepository employeeRepository, ILogger<EmployeesService> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Employee> CreateEmployee(Employee employee)
        {
            try
            {
                if(employee == null)
                {
                    throw new ArgumentNullException(nameof(employee));
                }
                return await _employeeRepository.CreateEmployee(employee);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call Create Employee in service class, Error Message = {ex}.");
                throw;
            }
        }
        public async Task<Object> UpdateEmployee(Employee employee, int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentNullException(nameof(id));
                }
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee));
                }
                return await _employeeRepository.UpdateEmployee(employee,id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call Update Employee in service class, Error Message = {ex}.");
                throw;
            }
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentNullException(nameof(id));
                }
                return await _employeeRepository.DeleteEmployee(id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call Delete Employee in service class, Error Message = {ex}.");
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                /*throw new ArgumentNullException();*/
                return await _employeeRepository.GetAllEmployees();

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call Get All Employees in service class, Error Message = {ex}.");
                throw;
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                if(id<=0)
                {
                    throw new ArgumentNullException(nameof(id));
                }    
                return await _employeeRepository.GetEmployeeById(id);

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call Get Employee By Id in service class, Error Message = {ex}.");
                throw;
            }
        }
    }
}
