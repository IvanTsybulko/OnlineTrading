using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Service.DTOs.BankAccount;

namespace OnlineTrading.Service.Interfaces
{
    public interface IBankAccountService
    {
        Task<BankAccountInfo> RetrieveById(int id);
        Task<List<BankAccountInfo>> RetrieveAll();
        Task<List<BankAccountInfo>> RetrieveByUserId(int userId);
        Task<int> CreateAsync(CreateBankAccountRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateBankAccountRequest request);
    }

}
