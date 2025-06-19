using System.Data.SqlTypes;

namespace OnlineTrading.Repository.Interfaces.User
{
    public class UserUpdate
    {
        public SqlString? Password { get; set; }
        public SqlString? FullName { get; set; }
        public SqlString? Role { get; set; }
    }
}