using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Repository.Base.Interfaces;

namespace OnlineTrading.Repository.Interfaces.BankAccount
{
    public interface IBankAccountRepository
        : ICreateRepository<Models.BankAccount>,
        IRetriveRepository<Models.BankAccount, BankAccountFilter>,
        IUpdateRepository<Models.BankAccount, BankAccountUpdate>,
        IDeleteRepository<Models.BankAccount>
    {
    }
}
