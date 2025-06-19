using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTrading.Repository.Base.Interfaces;

namespace OnlineTrading.Repository.Interfaces.User
{
    public interface IUserRepository
        : ICreateRepository<Models.User>,
        IRetriveRepository<Models.User, UserFilter>,
        IUpdateRepository<Models.User, UserUpdate>,
        IDeleteRepository<Models.User>
    {
    }
}
