using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    public class User : DomainObject
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public override string ToString() => Username;
    }
}
