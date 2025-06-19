using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Service.DTOs.Order
{
    public class CreateOrderRequest
    {
        public int CreatorId { get; set; }
        public int ShopId { get; set; }
        public int DeliveryServiceId { get; set; }
        public int BankAccountId { get; set; }
    }
}
