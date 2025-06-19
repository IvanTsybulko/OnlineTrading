using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Service.DTOs.User
{
    public class UpdateUserRequest
    {
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
