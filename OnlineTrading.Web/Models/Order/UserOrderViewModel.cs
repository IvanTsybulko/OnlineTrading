using OnlineTrading.Web.Models.Product;

namespace OnlineTrading.Web.Models.Order
{
    public class UserOrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryServiceName { get; set; }
        public string CreatorName { get; set; }
        public string ShopName { get; set; }
        public string BankName { get; set; }
        public decimal TotalCost { get; set; }
    }
}
