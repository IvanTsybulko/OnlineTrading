using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Repository.Base.Interfaces
{
    public interface IUpdateRepository<T,TUpdate> where T : class
    {
        Task<bool> UpdateAsync(int objectId, TUpdate update);
    }
}
