using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Models;
using OnlineTrading.Repository.Interfaces.Offer;
using OnlineTrading.Service.DTOs.Offer;
using OnlineTrading.Service.Interfaces;

namespace OnlineTrading.Service.Implementations
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        private OfferInfo MapToOfferInfo(Offer entity)
        {
            return new OfferInfo
            {
                OfferId = entity.OfferId,
                NewPrice = entity.NewPrice,
                ProductId = entity.ProductId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
        }

        public async Task<List<OfferInfo>> RetrieveAll()
        {
            var offers = await _offerRepository.RetrieveCollectionAsync(new OfferFilter()).ToListAsync();
            return offers.Select(MapToOfferInfo).ToList();
        }

        public async Task<OfferInfo> RetrieveById(int offerId)
        {
            var offer = await _offerRepository.RetrieveAsync(offerId);
            return MapToOfferInfo(offer);
        }

        public async Task<List<OfferInfo>> RetrieveByProductId(int productId)
        {
            var filter = new OfferFilter { ProductId = productId };
            var offers = await _offerRepository.RetrieveCollectionAsync(filter).ToListAsync();
            return offers.Select(MapToOfferInfo).ToList();
        }

        public async Task<int> CreateAsync(CreateOfferRequest request)
        {
            var offer = new Offer
            {
                NewPrice = request.NewPrice,
                ProductId = request.ProductId,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };
            return await _offerRepository.CreateAsync(offer);
        }

        public async Task<bool> DeleteAsync(int offerId)
        {
            return await _offerRepository.DeleteAsync(offerId);
        }

        public async Task<bool> UpdateAsync(int offerId, UpdateOfferRequest update)
        {
            OfferUpdate offerUpdate = new OfferUpdate
            {
                NewPrice = update.NewPrice,
                StartDate = update.StartDate,
                EndDate = update.EndDate
            };

            return await _offerRepository.UpdateAsync(offerId, offerUpdate);
        }
    }
}
