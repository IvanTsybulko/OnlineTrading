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
using OnlineTrading.Repository.Interfaces.Shop;

namespace OnlineTrading.Repository.Implementations
{
    public class ShopRepository : BaseRepository<Models.Shop>, IShopRepository
    {
        private const string idDbFieldName = "ShopId";

        public override string[] GetColumns()
        {
            return new string[]
            {
                "ShopId",
                "Name",
                "Location"
            };
        }

        public override string GetTableName()
        {
            return "Shops";
        }

        public override Shop MapEntity(SqlDataReader reader)
        {
            return new Shop
            {
                ShopId = Convert.ToInt32(reader["ShopId"]),
                Name = reader["Name"].ToString(),
                Location = reader["Location"].ToString()
            };
        }

        public async Task<int> CreateAsync(Shop entity)
        {
            return await base.CreateAsync(entity, idDbFieldName);
        }

        public async Task<bool> DeleteAsync(int objectId)
        {
            return await base.DeleteAsync(idDbFieldName, objectId);
        }

        public async Task<Shop> RetrieveAsync(int objectId)
        {
            return await base.RetrieveAsync(idDbFieldName, objectId);
        }

        public IAsyncEnumerable<Shop> RetrieveCollectionAsync(ShopFilter filter)
        {
            Filter filterCommand = new Filter();

            if (filter.Name!=null)
            {
                filterCommand.AddCondition("Name", filter.Name);
            }

            return base.RetrieveCollectionAsync(filterCommand);
        }

        public async Task<bool> UpdateAsync(int objectId, ShopUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.GetOpenConnection();

            UpdateCommand updateCommand = new UpdateCommand(connection, GetTableName(), idDbFieldName, objectId);

            if (update.Name != null)
                updateCommand.AddSetClause("Name", update.Name);

            if (update.Location != null)
                updateCommand.AddSetClause("Location", update.Location);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}
