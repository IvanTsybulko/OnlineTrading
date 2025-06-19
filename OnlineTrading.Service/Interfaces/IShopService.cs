using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Service.DTOs.Shop;

namespace OnlineTrading.Service.Interfaces
{
    public interface IShopService
    {
        Task<ShopInfo> RetrieveById(int id);
        Task<List<ShopInfo>> RetrieveAll();
        Task<int> CreateAsync(CreateShopRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateShopRequest request);
    }

}
