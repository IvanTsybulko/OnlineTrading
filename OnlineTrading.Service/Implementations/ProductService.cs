using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Models;
using OnlineTrading.Repository.Interfaces.Product;
using OnlineTrading.Service.DTOs.Product;
using OnlineTrading.Service.Interfaces;

namespace OnlineTrading.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        private ProductInfo Map(Product entity) => new ProductInfo
        {
            ProductId = entity.ProductId,
            Name = entity.Name,
            Class = entity.Class,
            Price = entity.Price
        };

        public async Task<List<ProductInfo>> RetrieveAll() =>
            (await _productRepository.RetrieveCollectionAsync(new ProductFilter()).ToListAsync())
            .Select(Map).ToList();

        public async Task<ProductInfo> RetrieveById(int id) => Map(await _productRepository.RetrieveAsync(id));

        public async Task<List<ProductInfo>> RetrieveByName(string name)
        {
            var filter = new ProductFilter { Name = name };
            return (await _productRepository.RetrieveCollectionAsync(filter).ToListAsync())
                .Select(Map).ToList();
        }

        public Task<int> CreateAsync(CreateProductRequest req) =>
            _productRepository.CreateAsync(new Product { Name = req.Name, Class = req.Class, Price = req.Price });

        public Task<bool> DeleteAsync(int id) => _productRepository.DeleteAsync(id);

        public Task<bool> UpdateAsync(int id, UpdateProductRequest req) =>
            _productRepository.UpdateAsync(id, new ProductUpdate { Name = req.Name, Class = req.Class, Price = req.Price });
    }
}
