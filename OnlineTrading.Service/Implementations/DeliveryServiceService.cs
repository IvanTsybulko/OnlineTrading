using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Models;
using OnlineTrading.Repository.Interfaces.DeliveryService;
using OnlineTrading.Service.DTOs.DeliveryService;
using OnlineTrading.Service.Interfaces;

namespace OnlineTrading.Service.Implementations
{
    public class DeliveryServiceService : IDeliveryServiceService
    {
        private readonly IDeliveryServiceRepository _deliveryServiceRepository;

        public DeliveryServiceService(IDeliveryServiceRepository deliveryServiceRepository)
        {
            _deliveryServiceRepository = deliveryServiceRepository;
        }

        private DeliveryServiceInfo MapToDeliveryServiceInfo(DeliveryService entity)
        {
            return new DeliveryServiceInfo
            {
                DeliveryServiceId = entity.DeliveryServiceId,
                Name = entity.Name,
                Price = entity.Price
            };
        }

        public async Task<List<DeliveryServiceInfo>> RetrieveAll()
        {
            var services = await _deliveryServiceRepository.RetrieveCollectionAsync(new DeliveryServiceFilter()).ToListAsync();
            return services.Select(MapToDeliveryServiceInfo).ToList();
        }

        public async Task<DeliveryServiceInfo> RetrieveById(int serviceId)
        {
            var service = await _deliveryServiceRepository.RetrieveAsync(serviceId);
            return MapToDeliveryServiceInfo(service);
        }

        public async Task<List<DeliveryServiceInfo>> RetrieveByName(string name)
        {
            var filter = new DeliveryServiceFilter { Name = name };
            var services = await _deliveryServiceRepository.RetrieveCollectionAsync(filter).ToListAsync();
            return services.Select(MapToDeliveryServiceInfo).ToList();
        }

        public async Task<int> CreateAsync(CreateDeliveryServiceRequest request)
        {
            var service = new DeliveryService
            {
                Name = request.Name,
                Price = request.Price
            };
            return await _deliveryServiceRepository.CreateAsync(service);
        }

        public async Task<bool> DeleteAsync(int serviceId)
        {
            return await _deliveryServiceRepository.DeleteAsync(serviceId);
        }

        public async Task<bool> UpdateAsync(int serviceId, UpdateDeliveryServiceRequest update)
        {
            var updateEntity = new DeliveryServiceUpdate
            {
                Price = update.Price
            };
            return await _deliveryServiceRepository.UpdateAsync(serviceId, updateEntity);
        }
    }
}
