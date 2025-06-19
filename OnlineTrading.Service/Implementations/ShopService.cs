using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineTrading.Models;
using OnlineTrading.Repository;
using OnlineTrading.Repository.Helpers;
using OnlineTrading.Repository.Interfaces.Shop;
using OnlineTrading.Service.DTOs.Shop;
using OnlineTrading.Service.Interfaces;

namespace OnlineTrading.Service.Implementations
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;

        public ShopService(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        private ShopInfo MapToShopInfo(Shop entity)
        {
            return new ShopInfo
            {
                ShopId = entity.ShopId,
                Name = entity.Name,
                Location = entity.Location
            };
        }

        public async Task<List<ShopInfo>> RetrieveAll()
        {
            var shops = await _shopRepository.RetrieveCollectionAsync(new ShopFilter()).ToListAsync();
            return shops.Select(MapToShopInfo).ToList();
        }

        public async Task<ShopInfo> RetrieveById(int shopId)
        {
            var shop = await _shopRepository.RetrieveAsync(shopId);
            return MapToShopInfo(shop);
        }

        public async Task<List<ShopInfo>> RetrieveByName(string name)
        {
            var filter = new ShopFilter { Name = name };
            var shops = await _shopRepository.RetrieveCollectionAsync(filter).ToListAsync();
            return shops.Select(MapToShopInfo).ToList();
        }

        public async Task<int> CreateAsync(CreateShopRequest request)
        {
            var shop = new Shop
            {
                Name = request.Name,
                Location = request.Location
            };
            return await _shopRepository.CreateAsync(shop);
        }

        public async Task<bool> DeleteAsync(int shopId)
        {
            return await _shopRepository.DeleteAsync(shopId);
        }

        public async Task<bool> UpdateAsync(int shopId, UpdateShopRequest update)
        {
            ShopUpdate shopUpdate = new ShopUpdate
            {
                Location = update.Location,
                Name = update.Name,
            };

            return await _shopRepository.UpdateAsync(shopId, shopUpdate);
        }
    }
}
