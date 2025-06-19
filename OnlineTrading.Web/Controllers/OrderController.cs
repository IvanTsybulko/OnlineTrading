using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineTrading.Models;
using OnlineTrading.Service.DTOs.Order;
using OnlineTrading.Service.DTOs.Product;
using OnlineTrading.Service.DTOs.ProductOrder;
using OnlineTrading.Service.Implementations;
using OnlineTrading.Service.Interfaces;
using OnlineTrading.Web.Models.Order;

namespace OnlineTrading.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IShopService _shopService;
        private readonly IDeliveryServiceService _deliveryService;
        private readonly IBankAccountService _bankAccService;
        private readonly IProductOrderService _productOrderService;
        private readonly IUserService _userOrderService;


        public OrderController(
            IOrderService orderService,
            IProductService productService,
            IShopService shopService,
            IDeliveryServiceService deliveryService,
            IBankAccountService bankAccService,
            IProductOrderService productOrderService,
            IUserService userOrderService)
        {
            _orderService = orderService;
            _productService = productService;
            _shopService = shopService;
            _deliveryService = deliveryService;
            _bankAccService = bankAccService;
            _productOrderService = productOrderService;
            _userOrderService = userOrderService;
        }

        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userOrderService.RetrieveById(userId.Value);

            if(user.Role == "Moderator")
            {
                return RedirectToAction("ModeratorIndex", "Order");
            }

            var orders = await _orderService.RetrieveByUserId(userId.Value);
            List<UserOrderViewModel> model = new List<UserOrderViewModel>();

            foreach (var order in orders)
            {
                model.Add(new UserOrderViewModel
                {
                    DeliveryServiceName = order.DeliveryService.Name,
                    OrderDate =  order.CreatedOn,
                    OrderId = order.OrderId,
                    TotalCost = order.TotalCost,
                    CreatorName = order.Creator.FullName,
                    BankName = order.BankAccount.BankName,
                    ShopName = order.Shop.Name,
                });
            }
            return View(model);
        }

        public async Task<IActionResult> ModeratorIndex()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userOrderService.RetrieveById(userId.Value);

            var orders = await _orderService.RetrieveAll();
            List<UserOrderViewModel> model = new List<UserOrderViewModel>();

            foreach (var order in orders)
            {
                model.Add(new UserOrderViewModel
                {
                    DeliveryServiceName = order.DeliveryService.Name,
                    OrderDate = order.CreatedOn,
                    OrderId = order.OrderId,
                    TotalCost = order.TotalCost,
                    CreatorName = order.Creator.FullName,
                    BankName = order.BankAccount.BankName,
                    ShopName = order.Shop.Name,
                });
            }
            return View(model);
        }

        public async Task<IActionResult> AdminIndex()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userOrderService.RetrieveById(userId.Value);

            var orders = await _orderService.RetrieveAll();
            List<UserOrderViewModel> model = new List<UserOrderViewModel>();

            foreach (var order in orders)
            {
                model.Add(new UserOrderViewModel
                {
                    DeliveryServiceName = order.DeliveryService.Name,
                    OrderDate = order.CreatedOn,
                    OrderId = order.OrderId,
                    TotalCost = order.TotalCost,
                    CreatorName = order.Creator.FullName,
                    BankName = order.BankAccount.BankName,
                    ShopName = order.Shop.Name,
                });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var products = await _productService.RetrieveAll();
            var deliveryServices = await _deliveryService.RetrieveAll();
            var bankAccounts = await _bankAccService.RetrieveByUserId(userId.Value);
            var shops = await _shopService.RetrieveAll();

            var viewModel = new CreateOrderViewModel
            {

                Products = products.Select(p => new ProductSelectionViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.Name,
                    Price = p.Price,
                    Quantity = 0
                }).ToList(),

                AvailableDeliveryServices = deliveryServices.Select(ds => new SelectListItem
                {
                    Value = ds.DeliveryServiceId.ToString(),
                    Text = ds.Name
                }).ToList(),

                AvailableBankAccounts = bankAccounts.Select(ba => new SelectListItem
                {
                    Value = ba.BankAccountId.ToString(),
                    Text = ba.AccountNumbers
                }).ToList(),

                AvailableShops = shops.Select(s => new SelectListItem
                {
                    Value = s.ShopId.ToString(),
                    Text = s.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var selectedProducts = model.Products
                .Where(p => p.Quantity > 0)
                .ToList();

            if (!selectedProducts.Any())
            {
                ModelState.AddModelError("", "You must select at least one product.");
                return View(model);
            }

            var order = new CreateOrderRequest
            {
                CreatorId = userId.Value,
                DeliveryServiceId = model.SelectedDeliveryServiceId,
                BankAccountId = model.SelectedBankAccountId,
                ShopId = model.SelectedShopId,
            };

            int orderId = await _orderService.CreateAsync(order);

            foreach (var product in selectedProducts)
            {
                CreateProductOrderRequest request = new CreateProductOrderRequest()
                {
                    OrderId = orderId,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                };

                await _productOrderService.CreateAsync(request);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            // 1. Get the order
            var order = await _orderService.RetrieveById(id);
            if (order == null)
                return NotFound();

            // 2. Get product-order links
            var productOrders = await _productOrderService.RetrieveAll();
            productOrders = productOrders.Where(p => p.OrderId == id).ToList()
                ;
            if (productOrders == null || !productOrders.Any())
                return View(new OrderDetailsViewModel
                {
                    OrderId = order.OrderId,
                    OrderDate = order.CreatedOn,
                    DeliveryServiceName = order.DeliveryService?.Name ?? "N/A",
                    BankAccountNumber = order.BankAccount?.AccountNumbers ?? "N/A",
                    ShopName = order.Shop?.Name ?? "N/A",
                    Products = new List<ProductSelectionViewModel>()
                });

            // 3. Load all product data
            List<ProductInfo> products = new List<ProductInfo>();

            foreach (var product in productOrders)
            {
                products.Add(await _productService.RetrieveById(product.ProductId));
            }

            // 4. Build the view model
            var viewModel = new OrderDetailsViewModel
            {
                OrderId = order.OrderId,
                OrderDate = order.CreatedOn,
                DeliveryServiceName = order.DeliveryService?.Name ?? "N/A",
                BankAccountNumber = order.BankAccount?.AccountNumbers ?? "N/A",
                ShopName = order.Shop?.Name ?? "N/A",
                Products = productOrders.Select(po =>
                {
                    var product = products.FirstOrDefault(p => p.ProductId == po.ProductId);
                    return new ProductSelectionViewModel
                    {
                        ProductName = product?.Name ?? "Unknown",
                        Price = product?.Price ?? 0,
                        Quantity = po.Quantity
                    };
                }).ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Update(int id)
        {
            var order = await _orderService.RetrieveById(id);
            if (order == null)
                return NotFound();

            var deliveryServices = await _deliveryService.RetrieveAll();

            var model = new OrderUpdateViewModel
            {
                OrderId = order.OrderId,
                DeliveryServiceId = order.DeliveryServiceId,
                DeliveryServices = deliveryServices.Select(ds => new SelectListItem
                {
                    Value = ds.DeliveryServiceId.ToString(),
                    Text = ds.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrderUpdateViewModel model)
        {

            UpdateOrderRequest request = new UpdateOrderRequest()
            {
                DeliveryServiceId = model.DeliveryServiceId
            };

            await _orderService.UpdateAsync(model.OrderId,request);

            return RedirectToAction("Details", new { id = model.OrderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.RetrieveById(id);
            if (order == null)
                return NotFound();

            var productOrders = await _productOrderService.RetrieveAll();
            productOrders = productOrders.Where(p => p.OrderId == id).ToList();

            foreach (var productOrder in productOrders)
            {
                await _productOrderService.DeleteAsync(productOrder.ProductId,productOrder.OrderId);
            }

            await _orderService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
