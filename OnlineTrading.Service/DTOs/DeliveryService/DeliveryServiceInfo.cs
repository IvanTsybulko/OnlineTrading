using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Service.DTOs.DeliveryService
{
    public class DeliveryServiceInfo
    {
        public int DeliveryServiceId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
