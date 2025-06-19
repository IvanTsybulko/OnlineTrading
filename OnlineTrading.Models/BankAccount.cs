using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }
        public string AccountNumbers { get; set; }
        public int UserId { get; set; }
        public string BankName { get; set; }
    }
}
