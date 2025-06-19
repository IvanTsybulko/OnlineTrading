using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Repository.Base.Interfaces;

namespace OnlineTrading.Repository.Interfaces.ProductOrder
{
    public interface IProductOrderRepository 
        : ICreateRepository<Models.ProductOrder>,
        IRetriveRepository<Models.ProductOrder, ProductOrderFilter>
    {
        public Task<bool> DeleteAsync(int productId, int orderId);
    }
}
