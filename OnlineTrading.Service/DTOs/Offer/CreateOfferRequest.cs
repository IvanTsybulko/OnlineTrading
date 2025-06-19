using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Service.DTOs.Offer
{
    public class CreateOfferRequest
    {
        public int ProductId { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
