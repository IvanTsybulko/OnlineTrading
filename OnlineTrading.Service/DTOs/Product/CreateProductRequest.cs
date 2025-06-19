using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTrading.Service.DTOs.Product
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public decimal Price { get; set; }
    }
}
