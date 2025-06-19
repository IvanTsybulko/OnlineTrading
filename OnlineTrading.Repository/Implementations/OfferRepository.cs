using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineTrading.Models;
using OnlineTrading.Repository.Base.Implementations;
using OnlineTrading.Repository.Helpers;
using OnlineTrading.Repository.Interfaces.Offer;

namespace OnlineTrading.Repository.Implementations
{
    public class OfferRepository : BaseRepository<Offer>, IOfferRepository
    {
        private readonly string idDbFieldName = "OfferId";

        public override string[] GetColumns()
        {
            return new string[]
            {
                "OfferId",
                "NewPrice",
                "ProductId",
                "StartDate",
                "EndDate"
            };
        }

        public override string GetTableName()
        {
            return "Offers"; 
        }

        public override Offer MapEntity(SqlDataReader reader)
        {
            return new Offer
            {
                OfferId = Convert.ToInt32(reader["OfferId"]),
                NewPrice = Convert.ToDecimal(reader["NewPrice"]),
                ProductId = Convert.ToInt32(reader["ProductId"]),
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                EndDate = Convert.ToDateTime(reader["EndDate"])
            };
        }

        public async Task<int> CreateAsync(Offer entity)
        {
            return await base.CreateAsync(entity, idDbFieldName);
        }

        public async Task<bool> DeleteAsync(int objectId)
        {
            return await base.DeleteAsync(idDbFieldName, objectId);
        }

        public async Task<Offer> RetrieveAsync(int objectId)
        {
            return await base.RetrieveAsync(idDbFieldName, objectId);
        }

        public IAsyncEnumerable<Offer> RetrieveCollectionAsync(OfferFilter filter)
        {
            Filter filterCommand = new Filter();

            if (filter.ProductId.HasValue)
                filterCommand.AddCondition("ProductId", filter.ProductId.Value);

            return base.RetrieveCollectionAsync(filterCommand);
        }

        public async Task<bool> UpdateAsync(int objectId, OfferUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.GetOpenConnection();

            UpdateCommand updateCommand = new UpdateCommand(connection, GetTableName(), idDbFieldName, objectId);

            if (update.NewPrice.HasValue)
                updateCommand.AddSetClause("NewPrice", update.NewPrice.Value);
            if (update.StartDate.HasValue)
                updateCommand.AddSetClause("StartDate", update.StartDate.Value);
            if (update.EndDate.HasValue)
                updateCommand.AddSetClause("EndDate", update.EndDate.Value);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}
