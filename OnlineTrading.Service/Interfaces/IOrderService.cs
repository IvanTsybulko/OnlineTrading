using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineTrading.Repository.Base.Implementations;
using OnlineTrading.Repository.Helpers;
using OnlineTrading.Repository.Interfaces.Order;
using OnlineTrading.Repository;
using OnlineTrading.Models;
using OnlineTrading.Service.DTOs.Order;
using OnlineTrading.Service.Implementations.OnlineTrading.Service.Implementations;

namespace OnlineTrading.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderInfo> RetrieveById(int orderId);
        Task<List<OrderInfo>> RetrieveAll();
        Task<List<OrderInfo>> RetrieveByUserId(int userId);
        Task<List<OrderInfo>> RetrieveByShopId(int shopId);
        Task<int> CreateAsync(CreateOrderRequest request);
        Task<bool> DeleteAsync(int orderId);
        Task<bool> UpdateAsync(int orderId, UpdateOrderRequest update);
    }
}
