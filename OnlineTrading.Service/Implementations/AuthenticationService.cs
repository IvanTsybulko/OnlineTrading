using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Models;
using OnlineTrading.Repository.Implementations;
using OnlineTrading.Repository.Interfaces.User;
using OnlineTrading.Service.DTOs.Authentication;
using OnlineTrading.Service.Helpers;
using OnlineTrading.Service.Intterfaces;

namespace OnlineTrading.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _employeeRepository;

        public AuthenticationService(IUserRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Username and password are required"
                };
            }

            var hashedPassword = SecurityHelper.HashPassword(request.Password);
            var filter = new UserFilter { Username = new SqlString(request.Username) };

            var employees = await _employeeRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var employee = employees.SingleOrDefault();

            if (employee == null || employee.Password != hashedPassword)
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid username or password"
                };
            }

            return new LoginResponse
            {
                Success = true,
                EmployeeId = employee.UserId,
                FullName = employee.FullName
            };
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var filter = new UserFilter { Username = new SqlString(request.Username) };
            var existingUser = await _employeeRepository.RetrieveCollectionAsync(filter).ToListAsync();
            if (existingUser.Count() != 0)
                return (false);

            var hashedPassword = SecurityHelper.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                Password = hashedPassword,
                FullName = request.FullName,
                Role = request.Role
            };

            await _employeeRepository.CreateAsync(user);
            return (true);
        }

    }
}
