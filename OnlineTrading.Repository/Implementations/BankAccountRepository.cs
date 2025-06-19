using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineTrading.Models;
using OnlineTrading.Repository.Base.Implementations;
using OnlineTrading.Repository.Helpers;
using OnlineTrading.Repository.Interfaces.BankAccount;

namespace OnlineTrading.Repository.Implementations
{
    public class BankAccountRepository : BaseRepository<BankAccount>, IBankAccountRepository
    {
        private readonly string idDbFieldName = "BankAccountId";

        public override string[] GetColumns()
        {
            return new string[]
            {
                "BankAccountId",
                "AccountNumbers",
                "UserId",
                "BankName"
            };
        }

        public override string GetTableName()
        {
            return "BankAccounts"; 
        }

        public override BankAccount MapEntity(SqlDataReader reader)
        {
            return new BankAccount
            {
                BankAccountId = Convert.ToInt32(reader["BankAccountId"]),
                AccountNumbers = reader["AccountNumbers"].ToString(),
                UserId = Convert.ToInt32(reader["UserId"]),
                BankName = reader["BankName"].ToString()
            };
        }

        public async Task<int> CreateAsync(BankAccount entity)
        {
            return await base.CreateAsync(entity, idDbFieldName);
        }

        public async Task<bool> DeleteAsync(int objectId)
        {
            return await base.DeleteAsync(idDbFieldName, objectId);
        }

        public async Task<BankAccount> RetrieveAsync(int objectId)
        {
            return await base.RetrieveAsync(idDbFieldName, objectId);
        }

        public IAsyncEnumerable<BankAccount> RetrieveCollectionAsync(BankAccountFilter filter)
        {
            Filter filterCommand = new Filter();

            if (filter.AccountNumbers != null)
                filterCommand.AddCondition("AccountNumbers", filter.AccountNumbers);

            if (filter.UserId.HasValue)
                filterCommand.AddCondition("UserId", filter.UserId.Value);

            return base.RetrieveCollectionAsync(filterCommand);
        }

        public async Task<bool> UpdateAsync(int objectId, BankAccountUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.GetOpenConnection();

            UpdateCommand updateCommand = new UpdateCommand(connection, GetTableName(), idDbFieldName, objectId);


            if (update.BankName != null)
                updateCommand.AddSetClause("BankName", update.BankName);


            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}
