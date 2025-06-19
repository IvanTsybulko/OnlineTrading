using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.ProductOrder
{
    public class ProductOrderFilter
    {
        public SqlInt32? ProductId { get; set; }
        public SqlInt32? OrderId { get; set; }
    }
}