using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.Product
{
    public class ProductUpdate
    {
        public SqlString? Name { get; set; }
        public SqlString? Class { get; set; }
        public SqlDecimal? Price { get; set; }
    }
}