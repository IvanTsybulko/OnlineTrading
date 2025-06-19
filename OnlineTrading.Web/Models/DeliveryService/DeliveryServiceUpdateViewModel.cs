namespace OnlineTrading.Web.Models.DeliveryService
{
    using System.ComponentModel.DataAnnotations;

    public class DeliveryServiceUpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Delivery fee must be greater than 0.")]
        public decimal Price { get; set; }
    }

}
