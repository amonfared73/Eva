using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using System.Text.Json.Serialization;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    [EvaTable("Users", Schema = "master")]
    public class User : ModelBase
    {
        public string Username { get; set; } = string.Empty;
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        [JsonIgnore]
        public string Signature { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<UserRoleMapping> UserRoleMappings { get; set; }
        [JsonIgnore]
        public ICollection<EvaLog> EvaLogs { get; set; }
        public override string ToString() => Username;
        public static implicit operator string(User user) => user.Username;
    }
}
