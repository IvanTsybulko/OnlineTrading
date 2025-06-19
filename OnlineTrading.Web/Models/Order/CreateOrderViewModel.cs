using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineTrading.Web.Models.Order
{
    public class CreateOrderViewModel
    {
        public int SelectedDeliveryServiceId { get; set; }
        public int SelectedBankAccountId { get; set; }
        public int SelectedShopId { get; set; }

        public List<SelectListItem> AvailableDeliveryServices { get; set; }
        public List<SelectListItem> AvailableBankAccounts { get; set; }
        public List<SelectListItem> AvailableShops { get; set; }

        public List<ProductSelectionViewModel> Products { get; set; }
    }
}
