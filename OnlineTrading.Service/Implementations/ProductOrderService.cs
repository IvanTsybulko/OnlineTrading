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
    using global::OnlineTrading.Repository.Interfaces.ProductOrder;
    using global::OnlineTrading.Service.DTOs.ProductOrder;
    using global::OnlineTrading.Service.Interfaces;

    namespace OnlineTrading.Service.Implementations
    {
        public class ProductOrderService : IProductOrderService
        {
            private readonly IProductOrderRepository _productOrderRepository;

            public ProductOrderService(IProductOrderRepository productOrderRepository)
            {
                _productOrderRepository = productOrderRepository;
            }

            private ProductOrderInfo MapToInfo(ProductOrder entity)
            {
                return new ProductOrderInfo
                {
                    ProductId = entity.ProductId,
                    OrderId = entity.OrderId,
                    Quantity = entity.Quantity
                };
            }

            public async Task<int> CreateAsync(CreateProductOrderRequest request)
            {
                var entity = new ProductOrder
                {
                    ProductId = request.ProductId,
                    OrderId = request.OrderId,
                    Quantity = request.Quantity
                };
                return await _productOrderRepository.CreateAsync(entity);
            }

            public async Task<bool> DeleteAsync(int productId, int orderId)
            {
                return await _productOrderRepository.DeleteAsync(productId, orderId);
            }

            public async Task<List<ProductOrderInfo>> RetrieveAll()
            {
                var productOrders = await _productOrderRepository.RetrieveCollectionAsync(new ProductOrderFilter()).ToListAsync();
                return productOrders.Select(MapToInfo).ToList();
            }

            Task<ProductOrderInfo> IProductOrderService.RetrieveByIds(int productId, int orderId)
            {
                throw new NotImplementedException();
            }
        }
    }

}
