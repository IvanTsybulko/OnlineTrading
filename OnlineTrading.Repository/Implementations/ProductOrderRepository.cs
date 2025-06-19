using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineTrading.Models;
using OnlineTrading.Repository.Base.Implementations;
using OnlineTrading.Repository.Helpers;
using OnlineTrading.Repository.Interfaces.ProductOrder;

namespace OnlineTrading.Repository.Implementations
{
    public class ProductOrderRepository : BaseRepository<ProductOrder>, IProductOrderRepository
    {
        private readonly string[] idDbFieldNames = { "ProductId", "OrderId" };

        public override string[] GetColumns()
        {
            return new string[]
            {
                "ProductId",
                "OrderId",
                "Quantity"
            };
        }

        public override string GetTableName()
        {
            return "ProductOrders"; 
        }

        public override ProductOrder MapEntity(SqlDataReader reader)
        {
            return new ProductOrder
            {
                ProductId = Convert.ToInt32(reader["ProductId"]),
                OrderId = Convert.ToInt32(reader["OrderId"]),
                Quantity = Convert.ToInt32(reader["Quantity"])
            };
        }

        public async Task<int> CreateAsync(ProductOrder entity)
        {
            return await base.CreateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int productId, int orderId)
        {
            using SqlConnection connection = await ConnectionFactory.GetOpenConnection();
            string sql = $"DELETE FROM {GetTableName()} WHERE ProductId = @ProductId AND OrderId = @OrderId";

            using SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ProductId", productId);
            cmd.Parameters.AddWithValue("@OrderId", orderId);

            int affected = await cmd.ExecuteNonQueryAsync();
            return affected > 0;
        }

        public async Task<ProductOrder> RetrieveAsync(int productId, int? orderId)
        {
            using SqlConnection connection = await ConnectionFactory.GetOpenConnection();
            string sql = $"SELECT * FROM {GetTableName()} WHERE ProductId = @ProductId AND OrderId = @OrderId";

            using SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ProductId", productId);
            cmd.Parameters.AddWithValue("@OrderId", orderId);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapEntity(reader);
            }
            return null;
        }

        public IAsyncEnumerable<ProductOrder> RetrieveCollectionAsync(ProductOrderFilter filter)
        {
            Filter filterCommand = new Filter();

            if (filter.ProductId.HasValue)
                filterCommand.AddCondition("ProductId", filter.ProductId.Value);

            if (filter.OrderId.HasValue)
                filterCommand.AddCondition("OrderId", filter.OrderId.Value);

            return base.RetrieveCollectionAsync(filterCommand);
        }

        public Task<ProductOrder> RetrieveAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}
