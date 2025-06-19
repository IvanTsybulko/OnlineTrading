using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Service.DTOs.DeliveryService
{
    public class UpdateDeliveryServiceRequest
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
    }
}
