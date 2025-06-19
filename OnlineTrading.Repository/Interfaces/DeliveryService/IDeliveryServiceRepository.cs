using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Repository.Base.Interfaces;

namespace OnlineTrading.Repository.Interfaces.DeliveryService
{
    public interface IDeliveryServiceRepository
        : ICreateRepository<Models.DeliveryService>,
        IRetriveRepository<Models.DeliveryService, DeliveryServiceFilter>,
        IUpdateRepository<Models.DeliveryService, DeliveryServiceUpdate>,
        IDeleteRepository<Models.DeliveryService>
    {
    }
}
