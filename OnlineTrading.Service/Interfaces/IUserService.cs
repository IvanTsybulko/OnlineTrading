using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Service.DTOs.User;

namespace OnlineTrading.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserInfo> RetrieveById(int id);
        Task<List<UserInfo>> RetrieveAll();
        Task<int> CreateAsync(CreateUserRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateUserRequest request);
    }

}
