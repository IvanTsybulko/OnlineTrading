using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CreatorId { get; set; }
        public int ShopId { get; set; }
        public int DeliveryServiceId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int BankAccountId { get; set; }
    }
}
