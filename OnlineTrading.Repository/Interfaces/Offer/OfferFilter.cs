using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.Offer
{
    public class OfferFilter
    {
        public SqlInt32? ProductId { get; set; }
    }
}