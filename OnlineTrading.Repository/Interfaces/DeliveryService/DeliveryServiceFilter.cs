using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.DeliveryService
{
    public class DeliveryServiceFilter
    {
        public SqlString? Name { get; set; }
    }
}