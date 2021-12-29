using EmployeeService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Context
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> option) : base(option)
        {
            SeedData();
        }
        public virtual DbSet<Employee> Employees { get; set; } 
        private void SeedData()
        {
            var employees = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Bao", Address = "Dak Lak", Picture = "none", Level = "Intern", EmployeeId = Guid.NewGuid(), CreatedAt = new DateTime(2021,1,1), UpdatedAt = new DateTime(2021, 1, 1) },
                new Employee() { Id = 2, Name = "Tu", Address = "Ha Tinh", Picture = "none", Level = "Intern" , EmployeeId = Guid.NewGuid(), CreatedAt = new DateTime(2021, 1, 1), UpdatedAt = new DateTime(2021, 1, 1) }

            };
            Employees.AddRange(employees);
            /*SaveChanges();*/
        }
    }
}
