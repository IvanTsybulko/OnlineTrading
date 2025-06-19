using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.Order
{
    public class OrderFilter
    {
        public SqlInt32? CreatorId { get; set; }
        public SqlInt32? ShopId { get; set; }
        public SqlInt32? DeliveryServiceId { get; set; }
    }
}