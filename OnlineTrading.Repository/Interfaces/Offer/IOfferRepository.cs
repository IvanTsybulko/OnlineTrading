using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Repository.Base.Interfaces;

namespace OnlineTrading.Repository.Interfaces.Offer
{
    public interface IOfferRepository
        : ICreateRepository<Models.Offer>,
        IRetriveRepository<Models.Offer, OfferFilter>,
        IUpdateRepository<Models.Offer, OfferUpdate>,
        IDeleteRepository<Models.Offer>
    {
    }
}
