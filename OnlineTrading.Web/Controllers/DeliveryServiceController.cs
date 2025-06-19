using Microsoft.AspNetCore.Mvc;
using OnlineTrading.Models;
using OnlineTrading.Service.DTOs.DeliveryService;
using OnlineTrading.Service.Interfaces;
using OnlineTrading.Web.Models.DeliveryService;

namespace OnlineTrading.Web.Controllers
{
    public class DeliveryServiceController : Controller
    {
        private readonly IDeliveryServiceService _deliveryService;

        public DeliveryServiceController(IDeliveryServiceService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var services = await _deliveryService.RetrieveAll(); // Replace with your method

            var model = services.Select(ds => new DeliveryServiceListViewModel
            {
                Id = ds.DeliveryServiceId,
                Name = ds.Name,
                Price = ds.Price
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _deliveryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var service = await _deliveryService.RetrieveById(id);
            if (service == null) return NotFound();

            var model = new DeliveryServiceUpdateViewModel
            {
                Id = service.DeliveryServiceId,
                Name = service.Name,
                Price = service.Price
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(DeliveryServiceUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            UpdateDeliveryServiceRequest request = new UpdateDeliveryServiceRequest()
            { 
                Name = model.Name,
                Price = model.Price
            };


            await _deliveryService.UpdateAsync(model.Id, request);
            return RedirectToAction("Index");
        }

    }
}
