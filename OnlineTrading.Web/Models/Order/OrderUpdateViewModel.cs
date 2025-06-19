using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OnlineTrading.Web.Models.Order
{
    public class OrderUpdateViewModel
    {
        public int OrderId { get; set; }

        public int DeliveryServiceId { get; set; }

        public IEnumerable<SelectListItem> DeliveryServices { get; set; }
    }
}
