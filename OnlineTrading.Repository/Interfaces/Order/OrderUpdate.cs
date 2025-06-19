using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.Order
{
    public class OrderUpdate
    {
        public SqlInt32? DeliveryServiceId { get; set; }
    }
}