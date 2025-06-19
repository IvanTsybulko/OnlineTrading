using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Service.DTOs.ProductOrder;

namespace OnlineTrading.Service.Interfaces
{
    public interface IProductOrderService
    {
        Task<ProductOrderInfo> RetrieveByIds(int productId, int orderId);
        Task<List<ProductOrderInfo>> RetrieveAll();
        Task<int> CreateAsync(CreateProductOrderRequest request);
        Task<bool> DeleteAsync(int productId, int orderId);
    }

}
