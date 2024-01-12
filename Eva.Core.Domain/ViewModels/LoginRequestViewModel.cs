using System.ComponentModel.DataAnnotations;

namespace Eva.Core.Domain.ViewModels
{
    public class LoginRequestViewModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
