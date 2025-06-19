using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Repository.Base.Interfaces;
using OnlineTrading.Repository.Interfaces.User;

namespace OnlineTrading.Repository.Interfaces.Shop
{
    public interface IShopRepository
        : ICreateRepository<Models.Shop>,
        IRetriveRepository<Models.Shop, ShopFilter>,
        IUpdateRepository<Models.Shop, ShopUpdate>,
        IDeleteRepository<Models.Shop>
    {
    }
}
