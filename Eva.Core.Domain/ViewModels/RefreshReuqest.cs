using System.ComponentModel.DataAnnotations;

namespace Eva.Core.Domain.ViewModels
{
    public class RefreshReuqest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
