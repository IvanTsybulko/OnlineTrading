using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace OnlineTrading.Repository.Base.Interfaces
{
    public interface IBasicFunctions<T> where T : class
    {
        protected abstract string GetTableName();
        protected abstract string[] GetColumns();
        protected abstract T MapEntity(SqlDataReader reader);
    }
}
