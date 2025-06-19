using System.ComponentModel.DataAnnotations;

namespace OnlineTrading.Web.Models.Authentication
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(200)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; }

        public string Role { get; set; }
    }

}
