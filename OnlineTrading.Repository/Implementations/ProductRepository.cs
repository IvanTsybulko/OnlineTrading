using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using OnlineTrading.Models;
using OnlineTrading.Repository.Base.Implementations;
using OnlineTrading.Repository.Helpers;
using OnlineTrading.Repository.Interfaces.Product;

namespace OnlineTrading.Repository.Implementations
{
    public class ProductRepository : BaseRepository<Models.Product>, IProductRepository
    {
        private readonly string idDbFieldName = "ProductId";

        public override string[] GetColumns()
        {
            return new string[]
            {
                "ProductId",
                "Name",
                "Class",
                "Price"
            };
        }

        public override string GetTableName()
        {
            return "Products"; 
        }

        public override Product MapEntity(SqlDataReader reader)
        {
            return new Product
            {
                ProductId = Convert.ToInt32(reader["ProductId"]),
                Name = reader["Name"].ToString(),
                Class = reader["Class"].ToString(),
                Price = Convert.ToDecimal(reader["Price"])
            };
        }

        public async Task<int> CreateAsync(Product entity)
        {
            return await base.CreateAsync(entity, idDbFieldName);
        }

        public async Task<bool> DeleteAsync(int objectId)
        {
            return await base.DeleteAsync(idDbFieldName, objectId);
        }

        public async Task<Product> RetrieveAsync(int objectId)
        {
            return await base.RetrieveAsync(idDbFieldName, objectId);
        }

        public IAsyncEnumerable<Product> RetrieveCollectionAsync(ProductFilter filter)
        {
            Filter filterCommand = new Filter();

            if (filter.Name != null)
                filterCommand.AddCondition("Name", filter.Name);

            if (filter.Class != null)
                filterCommand.AddCondition("Class", filter.Class);

            return base.RetrieveCollectionAsync(filterCommand);
        }

        public async Task<bool> UpdateAsync(int objectId, ProductUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.GetOpenConnection();

            UpdateCommand updateCommand = new UpdateCommand(connection, GetTableName(), idDbFieldName, objectId);

            if (update.Name != null)
                updateCommand.AddSetClause("Name", update.Name);

            if (update.Class != null)
                updateCommand.AddSetClause("Class", update.Class);

            if (update.Price.HasValue)
                updateCommand.AddSetClause("Price", update.Price.Value);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}
