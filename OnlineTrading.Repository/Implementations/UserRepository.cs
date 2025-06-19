using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OnlineTrading.Models;
using OnlineTrading.Repository.Base.Implementations;
using OnlineTrading.Repository.Helpers;
using OnlineTrading.Repository.Interfaces.User;

namespace OnlineTrading.Repository.Implementations
{
    public class UserRepository
        : BaseRepository<Models.User>, IUserRepository
    {
        private readonly string idDbFieldName = "UserId";
        public override string[] GetColumns()
        {
            return new string[]
            {
                "UserId",
                "Username",
                "Password",
                "FullName",
                "Role"
            };
        }

        public override string GetTableName()
        {
            return "Users";
        }

        public override User MapEntity(SqlDataReader reader)
        {
            return new User
            {
                UserId = Convert.ToInt32(reader["UserId"]),
                Username = reader["Username"].ToString(),
                Password = reader["Password"].ToString(),
                FullName = reader["FullName"].ToString(),
                Role = reader["Role"].ToString()
            };
        }
        public async Task<int> CreateAsync(User entity)
        {
            return await base.CreateAsync(entity,idDbFieldName);
        }

        public async Task<bool> DeleteAsync(int objectId)
        {
            return await  base.DeleteAsync(idDbFieldName,objectId);
        }

        public async Task<User> RetrieveAsync(int objectId)
        {
            return await base.RetrieveAsync(idDbFieldName, objectId);
        }

        public IAsyncEnumerable<User> RetrieveCollectionAsync(UserFilter filter)
        {
            Filter filterCommand = new Filter();

            if(filter.Username != null)
            {
                filterCommand.AddCondition("Username",filter.Username);
            }

            return base.RetrieveCollectionAsync(filterCommand);
        }

        public async Task<bool> UpdateAsync(int objectId, UserUpdate update)
        {
            SqlConnection connection = await ConnectionFactory.GetOpenConnection();

            UpdateCommand updateCommand = new UpdateCommand(connection,GetTableName(),idDbFieldName,objectId);

            if(update.Password != null) 
                updateCommand.AddSetClause("Password",update.Password);
            if(update.FullName != null)
                updateCommand.AddSetClause("FullName",update.FullName);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}
