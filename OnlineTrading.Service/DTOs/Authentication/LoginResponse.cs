using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Service.DTOs.Authentication
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public int? EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
