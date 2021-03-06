using AutoMapper;
using EmployeeService.Core.Interfaces.Repositories;
using EmployeeService.Core.Models;
using EmployeeService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.Helpers;

namespace EmployeeService.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeRepository(EmployeeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            var dbEmployee = _mapper.Map<Entities.Employee>(employee);

            await _dbContext.Employees.AddAsync(dbEmployee);
            await _dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Object> UpdateEmployee(Employee employee, int id)
        {
            var dbEmployee = await _dbContext.Employees.FindAsync(id);

            if (dbEmployee == null || dbEmployee.Id != id)
            {
                return new { message = "Not found!" };
            }

            // Handle concurrency
            if (dbEmployee.UpdatedAt != employee.UpdatedAt)
            {
                return new { message = "Employee has been updated, please refresh the page!" };
            }
            
            dbEmployee = _mapper.Map<Entities.Employee>(employee);
            dbEmployee.UpdatedAt = DateTime.Now;

            // Update employee
            _dbContext.Employees.Update(dbEmployee);

            //Commit
            await _dbContext.SaveChangesAsync();

            return new { message = "Update success!", employee = dbEmployee };
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee != null)
            {
                // Delete employee
                _dbContext.Employees.Remove(employee);

                // Commit 
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return true;
        }

        public async Task<PagedList<Employee>> GetAllEmployees(EmployeeParameters employeeParameters)
        {
            var employees = await _dbContext.Employees.ToListAsync().ConfigureAwait(false);
            var result = _mapper.Map<List<Employee>>(employees);

            if (employees != null)
            {
                return PagedList<Employee>.ToPagedList(result, employeeParameters.PageNumber, employeeParameters.PageSize);
            }

            return null;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee != null)
            {
                return _mapper.Map<Employee>(employee);
            }

            return null;
        }
    }
}
