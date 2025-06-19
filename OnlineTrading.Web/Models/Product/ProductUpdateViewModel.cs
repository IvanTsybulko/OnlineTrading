using System.ComponentModel.DataAnnotations;

namespace OnlineTrading.Web.Models.Product
{
    public class ProductUpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public string? Class { get; set; }


        public decimal Price { get; set; }
    }

}
