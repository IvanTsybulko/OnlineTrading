using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Service.DTOs.Product;

namespace OnlineTrading.Service.Interfaces
{
    public interface IProductService
    {
        Task<ProductInfo> RetrieveById(int id);
        Task<List<ProductInfo>> RetrieveAll();
        Task<int> CreateAsync(CreateProductRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateProductRequest request);
    }

}
