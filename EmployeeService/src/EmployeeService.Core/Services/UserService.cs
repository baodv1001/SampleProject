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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<User> GetUserByUsername(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    throw new ArgumentNullException(nameof(username));
                }
                return await _userRepository.GetUserByUsername(username);

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error while trying to call Get Employee By Id in service class, Error Message = {ex}.");
                throw;
            }
        }
    }
}
