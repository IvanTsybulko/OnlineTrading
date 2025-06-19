using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Models;
using OnlineTrading.Repository.Interfaces.BankAccount;
using OnlineTrading.Service.DTOs.BankAccount;
using OnlineTrading.Service.Interfaces;

namespace OnlineTrading.Service.Implementations
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public BankAccountService(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        private BankAccountInfo MapToBankAccountInfo(BankAccount entity)
        {
            return new BankAccountInfo
            {
                BankAccountId = entity.BankAccountId,
                AccountNumbers = entity.AccountNumbers,
                UserId = entity.UserId,
                BankName = entity.BankName
            };
        }

        public async Task<List<BankAccountInfo>> RetrieveAll()
        {
            var accounts = await _bankAccountRepository.RetrieveCollectionAsync(new BankAccountFilter()).ToListAsync();
            return accounts.Select(MapToBankAccountInfo).ToList();
        }

        public async Task<BankAccountInfo> RetrieveById(int accountId)
        {
            var account = await _bankAccountRepository.RetrieveAsync(accountId);
            return MapToBankAccountInfo(account);
        }

        public async Task<List<BankAccountInfo>> RetrieveByUserId(int userId)
        {
            var filter = new BankAccountFilter { UserId = userId };
            var accounts = await _bankAccountRepository.RetrieveCollectionAsync(filter).ToListAsync();
            return accounts.Select(MapToBankAccountInfo).ToList();
        }

        public async Task<int> CreateAsync(CreateBankAccountRequest request)
        {
            var account = new BankAccount
            {
                AccountNumbers = request.AccountNumbers,
                UserId = request.UserId,
                BankName = request.BankName
            };
            return await _bankAccountRepository.CreateAsync(account);
        }

        public async Task<bool> DeleteAsync(int accountId)
        {
            return await _bankAccountRepository.DeleteAsync(accountId);
        }

        public async Task<bool> UpdateAsync(int accountId, UpdateBankAccountRequest update)
        {
            var updateEntity = new BankAccountUpdate
            {
                BankName = update.BankName
            };
            return await _bankAccountRepository.UpdateAsync(accountId, updateEntity);
        }
    }
}
