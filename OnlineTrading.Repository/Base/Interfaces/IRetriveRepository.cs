using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Repository.Base.Interfaces
{
    public interface IRetriveRepository<T,TFilter> where T : class 
    {
        Task<T> RetrieveAsync(int objectId);
        IAsyncEnumerable<T> RetrieveCollectionAsync(TFilter filter);
    }
}
