using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.Product
{
    public class ProductFilter
    {
        public SqlString? Name { get; set; }
        public SqlString? Class { get; set; }
    }
}