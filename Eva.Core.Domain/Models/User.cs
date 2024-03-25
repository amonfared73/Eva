using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using System.Text.Json.Serialization;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class User : ModelBase
    {
        public string Username { get; set; } = string.Empty;
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        [JsonIgnore]
        public string Signature { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<UserRoleMapping> UserRoleMappings { get; set; }
        [JsonIgnore]
        public ICollection<EvaLog> EvaLogs { get; set; }
        public override string ToString() => Username;
    }
}
