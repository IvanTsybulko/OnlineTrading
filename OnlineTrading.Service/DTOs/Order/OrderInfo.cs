using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Service.DTOs.Order
{
    public class OrderInfo
    {
        public int OrderId { get; set; }
        public int CreatorId { get; set; }
        public Models.User Creator { get; set; }
        public int ShopId { get; set; }
        public Models.Shop Shop { get; set; }
        public int DeliveryServiceId { get; set; }
        public Models.DeliveryService DeliveryService { get; set; }
        public DateTime CreatedOn { get; set; }
        public int BankAccountId { get; set; }
        public Models.BankAccount BankAccount { get; set; }
        public decimal TotalCost { get; set; }
    }
}
