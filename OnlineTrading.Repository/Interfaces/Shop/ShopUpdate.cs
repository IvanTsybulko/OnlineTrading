using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.Shop
{
    public class ShopUpdate
    {
        public SqlString? Name { get; set; }
        public SqlString? Location { get; set; }
    }
}