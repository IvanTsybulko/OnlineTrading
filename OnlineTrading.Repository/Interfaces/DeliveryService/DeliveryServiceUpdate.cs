using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.DeliveryService
{
    public class DeliveryServiceUpdate
    {
        public SqlDecimal? Price { get; set; }
    }
}