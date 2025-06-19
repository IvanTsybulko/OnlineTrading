using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Service.DTOs.BankAccount
{
    public class CreateBankAccountRequest
    {
        public string AccountNumbers { get; set; }
        public int UserId { get; set; }
        public string BankName { get; set; }
    }
}
