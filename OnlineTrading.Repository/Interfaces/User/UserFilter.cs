using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.User
{
    public class UserFilter
    {
        public SqlString? Username { get; set; }
    }
}