using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Repository.Base.Interfaces;
using OnlineTrading.Repository.Interfaces.User;

namespace OnlineTrading.Repository.Interfaces.Product
{
    public interface IProductRepository
        : ICreateRepository<Models.Product>,
        IRetriveRepository<Models.Product, ProductFilter>,
        IUpdateRepository<Models.Product, ProductUpdate>,
        IDeleteRepository<Models.Product>
    {
    }
}
