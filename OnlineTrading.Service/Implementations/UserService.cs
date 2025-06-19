using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Service.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using global::OnlineTrading.Models;
    using global::OnlineTrading.Repository.Interfaces.User;
    using global::OnlineTrading.Service.DTOs.User;
    using global::OnlineTrading.Service.Interfaces;

    namespace OnlineTrading.Service.Implementations
    {
        public class UserService : IUserService
        {
            private readonly IUserRepository _userRepository;

            public UserService(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            private UserInfo MapToInfo(User entity)
            {
                return new UserInfo
                {
                    UserId = entity.UserId,
                    Username = entity.Username,
                    FullName = entity.FullName,
                    Role = entity.Role
                };
            }

            public async Task<List<UserInfo>> RetrieveAll()
            {
                var users = await _userRepository.RetrieveCollectionAsync(new UserFilter()).ToListAsync();
                return users.Select(MapToInfo).ToList();
            }

            public async Task<UserInfo> RetrieveById(int userId)
            {
                var user = await _userRepository.RetrieveAsync(userId);
                return MapToInfo(user);
            }

            public async Task<List<UserInfo>> RetrieveByUsername(string username)
            {
                var filter = new UserFilter { Username = username };
                var users = await _userRepository.RetrieveCollectionAsync(filter).ToListAsync();
                return users.Select(MapToInfo).ToList();
            }

            public async Task<int> CreateAsync(CreateUserRequest request)
            {
                var user = new User
                {
                    Username = request.Username,
                    Password = request.Password,
                    FullName = request.FullName,
                    Role = request.Role
                };
                return await _userRepository.CreateAsync(user);
            }

            public async Task<bool> DeleteAsync(int userId)
            {
                return await _userRepository.DeleteAsync(userId);
            }

            public async Task<bool> UpdateAsync(int userId, UpdateUserRequest update)
            {
                var userUpdate = new UserUpdate
                {
                    FullName = update.FullName,
                    Password = update.Password
                };
                return await _userRepository.UpdateAsync(userId, userUpdate);
            }
        }
    }

}
