using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Service.DTOs.Offer;

namespace OnlineTrading.Service.Interfaces
{
    public interface IOfferService
    {
        Task<OfferInfo> RetrieveById(int id);
        Task<List<OfferInfo>> RetrieveAll();
        Task<int> CreateAsync(CreateOfferRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateOfferRequest request);
    }

}
