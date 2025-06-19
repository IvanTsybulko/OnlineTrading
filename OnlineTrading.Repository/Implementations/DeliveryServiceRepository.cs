using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineTrading.Models;
using OnlineTrading.Repository.Base.Implementations;
using OnlineTrading.Repository.Helpers;
using OnlineTrading.Repository.Interfaces.DeliveryService;

namespace OnlineTrading.Repository.Implementations
{
    public class DeliveryServiceRepository : BaseRepository<DeliveryService>, IDeliveryServiceRepository
    {
        private readonly string idDbFieldName = "DeliveryServiceId";

        public override string[] GetColumns()
        {
            return new string[]
            {
                "DeliveryServiceId",
                "Name",
                "Price"
            };
        }

        public override string GetTableName()
        {
            return "DeliveryServices"; 
        }

        public override DeliveryService MapEntity(SqlDataReader reader)
        {
            return new DeliveryService
            {
                DeliveryServiceId = Convert.ToInt32(reader["DeliveryServiceId"]),
                Name = reader["Name"].ToString(),
                Price = Convert.ToDecimal(reader["Price"])
            };
        }

        public async Task<int> CreateAsync(DeliveryService entity)
        {
            return await base.CreateAsync(entity, idDbFieldName);
        }

        public async Task<bool> DeleteAsync(int objectId)
        {
            return await base.DeleteAsync(idDbFieldName, objectId);
        }

        public async Task<DeliveryService> RetrieveAsync(int objectId)
        {
            return await base.RetrieveAsync(idDbFieldName, objectId);
        }

        public IAsyncEnumerable<DeliveryService> RetrieveCollectionAsync(DeliveryServiceFilter filter)
        {
            Filter filterCommand = new Filter();

            if (filter.Name != null)
                filterCommand.AddCondition("Name", filter.Name);

            return base.RetrieveCollectionAsync(filterCommand);
        }

        public async Task<bool> UpdateAsync(int objectId, DeliveryServiceUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.GetOpenConnection();

            UpdateCommand updateCommand = new UpdateCommand(connection, GetTableName(), idDbFieldName, objectId);

            if (update.Price.HasValue)
                updateCommand.AddSetClause("Price", update.Price.Value);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}
