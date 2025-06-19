using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Repository.Base.Interfaces;

namespace OnlineTrading.Repository.Interfaces.Order
{
    public interface IOrderRepository
        : ICreateRepository<Models.Order>,
        IRetriveRepository<Models.Order, OrderFilter>,
        IUpdateRepository<Models.Order, OrderUpdate>,
        IDeleteRepository<Models.Order>
    {

    }
}
