using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.BankAccount
{
    public class BankAccountFilter
    {
        public SqlInt32? UserId { get; set; }
        public SqlString? AccountNumbers { get; set; }
    }
}