using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.Offer
{
    public class OfferUpdate
    {
        public SqlDecimal? NewPrice { get; set; }
        public SqlDateTime? StartDate { get; set; }
        public SqlDateTime? EndDate { get; set; }
    }
}