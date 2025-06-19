using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Service.DTOs.DeliveryService;

namespace OnlineTrading.Service.Interfaces
{
    public interface IDeliveryServiceService
    {
        Task<DeliveryServiceInfo> RetrieveById(int id);
        Task<List<DeliveryServiceInfo>> RetrieveAll();
        Task<int> CreateAsync(CreateDeliveryServiceRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateDeliveryServiceRequest request);
    }

}
