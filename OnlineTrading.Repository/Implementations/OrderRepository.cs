using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineTrading.Models;
using OnlineTrading.Repository.Base.Implementations;
using OnlineTrading.Repository.Helpers;
using OnlineTrading.Repository.Interfaces.Order;

namespace OnlineTrading.Repository.Implementations
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly string idDbFieldName = "OrderId";

        public override string[] GetColumns()
        {
            return new string[]
            {
                "OrderId",
                "CreatorId",
                "ShopId",
                "DeliveryServiceId",
                "CreatedOn",
                "BankAccountId"
            };
        }

        public override string GetTableName()
        {
            return "Orders"; // Change if your actual table name differs
        }

        public override Order MapEntity(SqlDataReader reader)
        {
            return new Order
            {
                OrderId = Convert.ToInt32(reader["OrderId"]),
                CreatorId = Convert.ToInt32(reader["CreatorId"]),
                ShopId = Convert.ToInt32(reader["ShopId"]),
                DeliveryServiceId = Convert.ToInt32(reader["DeliveryServiceId"]),
                CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                BankAccountId = Convert.ToInt32(reader["BankAccountId"])
            };
        }

        public async Task<int> CreateAsync(Order entity)
        {
            return await base.CreateAsync(entity, idDbFieldName);
        }

        public async Task<bool> DeleteAsync(int objectId)
        {
            return await base.DeleteAsync(idDbFieldName, objectId);
        }

        public async Task<Order> RetrieveAsync(int objectId)
        {
            return await base.RetrieveAsync(idDbFieldName, objectId);
        }

        public IAsyncEnumerable<Order> RetrieveCollectionAsync(OrderFilter filter)
        {
            Filter filterCommand = new Filter();

            if (filter.CreatorId.HasValue)
                filterCommand.AddCondition("CreatorId", filter.CreatorId.Value);

            if (filter.ShopId.HasValue)
                filterCommand.AddCondition("ShopId", filter.ShopId.Value);

            if (filter.DeliveryServiceId.HasValue)
                filterCommand.AddCondition("DeliveryServiceId", filter.DeliveryServiceId.Value);

            return base.RetrieveCollectionAsync(filterCommand);
        }

        public async Task<bool> UpdateAsync(int objectId, OrderUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.GetOpenConnection();

            UpdateCommand updateCommand = new UpdateCommand(connection, GetTableName(), idDbFieldName, objectId);

            if (update.DeliveryServiceId.HasValue)
                updateCommand.AddSetClause("DeliveryServiceId", update.DeliveryServiceId.Value);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}
