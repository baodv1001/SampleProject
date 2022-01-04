using AutoMapper;
using EmployeeService.Core.Interfaces.Repositories;
using EmployeeService.Core.Models;
using EmployeeService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EmployeeDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserRepository(EmployeeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<User> GetUserByUsername(string username)
        {
            var user =  await _dbContext.Users.Include(i => i.Role).FirstOrDefaultAsync( i => i.Username == username);
            if (user != null)
            {
                return _mapper.Map<User>(user);
            }
            return null;
        }
    }
}
