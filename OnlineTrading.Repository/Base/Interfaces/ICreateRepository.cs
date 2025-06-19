using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Repository.Base.Interfaces
{
    public interface ICreateRepository<T> where T : class
    {
        Task<int> CreateAsync(T entity);
    }
}
