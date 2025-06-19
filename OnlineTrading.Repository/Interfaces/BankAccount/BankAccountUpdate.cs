using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.BankAccount
{
    public class BankAccountUpdate
    {
        public SqlString? BankName { get; set; }
    }
}