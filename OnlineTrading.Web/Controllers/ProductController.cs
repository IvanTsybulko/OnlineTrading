using Microsoft.AspNetCore.Mvc;
using OnlineTrading.Service.DTOs.Product;
using OnlineTrading.Service.Interfaces;
using OnlineTrading.Web.Models.Product;

namespace OnlineTrading.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.RetrieveAll();

            var model = products.Select(p => new ProductListViewModel
            {
                Id = p.ProductId,
                Name = p.Name,
                Price = p.Price
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.RetrieveById(id);
            if (product == null) return NotFound();

            var model = new ProductUpdateViewModel
            {
                Id = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Class = product.Class,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductUpdateViewModel model)
        {

            UpdateProductRequest request = new UpdateProductRequest()
            {
                Class = model.Class,
                Name = model.Name,
                Price = model.Price
            };

            await _productService.UpdateAsync(model.Id, request);

            return RedirectToAction("Index");
        }

    }
}
