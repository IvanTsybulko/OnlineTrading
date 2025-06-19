using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Service.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using global::OnlineTrading.Models;
    using global::OnlineTrading.Repository.Interfaces.Order;
    using global::OnlineTrading.Service.DTOs.Order;
    using global::OnlineTrading.Service.Interfaces;

    namespace OnlineTrading.Service.Implementations
    {
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Threading.Tasks;
        using global::OnlineTrading.Repository.Interfaces.BankAccount;
        using global::OnlineTrading.Repository.Interfaces.DeliveryService;
        using global::OnlineTrading.Repository.Interfaces.Product;
        using global::OnlineTrading.Repository.Interfaces.ProductOrder;
        using global::OnlineTrading.Repository.Interfaces.Shop;
        using global::OnlineTrading.Repository.Interfaces.User;

        public class OrderService : IOrderService
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IUserRepository _userRepository;
            private readonly IShopRepository _shopRepository;
            private readonly IDeliveryServiceRepository _deliveryRepository;
            private readonly IBankAccountRepository _bankRepository;
            private readonly IProductRepository _productRepository;
            private readonly IProductOrderRepository _productOrderRepository;



            public OrderService(IOrderRepository orderRepository,
                IUserRepository userRepository,
                IShopRepository shopRepository,
                IDeliveryServiceRepository deliveryRepository,
                IBankAccountRepository bankRepository,
                IProductRepository productRepository,
                IProductOrderRepository productOrderRepository)
            {
                _orderRepository = orderRepository;
                _userRepository = userRepository;
                _shopRepository = shopRepository;
                _deliveryRepository = deliveryRepository;
                _bankRepository = bankRepository;
                _productRepository = productRepository;
                _productOrderRepository = productOrderRepository;

            }

            private async Task<OrderInfo> MapToInfo(Order entity)
            {
                var creator = await _userRepository.RetrieveAsync(entity.CreatorId);
                var shop = await _shopRepository.RetrieveAsync(entity.ShopId);
                var delivery = await _deliveryRepository.RetrieveAsync(entity.DeliveryServiceId);
                var bankAcc = await _bankRepository.RetrieveAsync(entity.BankAccountId);
                
                decimal totalCost = 0;
                var productOrders = 
                    await _productOrderRepository.RetrieveCollectionAsync(new ProductOrderFilter { OrderId = entity.OrderId }).ToListAsync();
                
                foreach (var productOrder in productOrders)
                {
                    var product = await _productRepository.RetrieveAsync(productOrder.ProductId);
                    totalCost += product.Price * productOrder.Quantity;
                }

                return new OrderInfo
                {
                    OrderId = entity.OrderId,
                    CreatorId = entity.CreatorId,
                    ShopId = entity.ShopId,
                    DeliveryServiceId = entity.DeliveryServiceId,
                    CreatedOn = entity.CreatedOn,
                    BankAccountId = entity.BankAccountId,
                    Creator = creator,
                    Shop = shop,
                    DeliveryService = delivery,
                    BankAccount = bankAcc,
                    TotalCost = totalCost,
                };
            }

            public async Task<int> CreateAsync(CreateOrderRequest request)
            {
                var order = new Order
                {
                    CreatorId = request.CreatorId,
                    ShopId = request.ShopId,
                    DeliveryServiceId = request.DeliveryServiceId,
                    CreatedOn = DateTime.Now,
                    BankAccountId = request.BankAccountId
                };

                return await _orderRepository.CreateAsync(order);
            }

            public async Task<bool> DeleteAsync(int orderId)
            {
                return await _orderRepository.DeleteAsync(orderId);
            }

            public async Task<List<OrderInfo>> RetrieveAll()
            {
                var orders = await _orderRepository.RetrieveCollectionAsync(new OrderFilter()).ToListAsync();
                var orderInfos = await Task.WhenAll(orders.Select(order => MapToInfo(order)));
                return orderInfos.ToList();
            }

            public async Task<OrderInfo> RetrieveById(int orderId)
            {
                var order = await _orderRepository.RetrieveAsync(orderId);
                return await MapToInfo(order);
            }

            public async Task<List<OrderInfo>> RetrieveByUserId(int userId)
            {
                var filter = new OrderFilter { CreatorId = userId };
                var orders = await _orderRepository.RetrieveCollectionAsync(filter).ToListAsync();
                var orderInfos = await Task.WhenAll(orders.Select(order => MapToInfo(order)));
                return orderInfos.ToList();
            }

            public async Task<List<OrderInfo>> RetrieveByShopId(int shopId)
            {
                var filter = new OrderFilter { ShopId = shopId };
                var orders = await _orderRepository.RetrieveCollectionAsync(filter).ToListAsync();
                var orderInfos = await Task.WhenAll(orders.Select(order => MapToInfo(order)));
                return orderInfos.ToList();
            }



            public async Task<bool> UpdateAsync(int orderId, UpdateOrderRequest update)
            {
                var updateModel = new OrderUpdate
                {
                    DeliveryServiceId = update.DeliveryServiceId,
                };

                return await _orderRepository.UpdateAsync(orderId, updateModel);
            }
        }
    }
}
