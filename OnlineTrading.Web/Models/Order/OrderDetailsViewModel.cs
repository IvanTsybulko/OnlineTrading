namespace OnlineTrading.Web.Models.Order
{
    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryServiceName { get; set; }
        public string BankAccountNumber { get; set; }
        public string ShopName { get; set; }

        public List<ProductSelectionViewModel> Products { get; set; } = new();

        public decimal TotalPrice => Products?.Sum(p => p.Price * p.Quantity) ?? 0;
    }
}
