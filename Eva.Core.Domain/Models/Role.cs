using Eva.Core.Domain.BaseModels;
using System.Text.Json.Serialization;

namespace Eva.Core.Domain.Models
{
    public class Role : DomainObject
    {
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<UserRoleMapping> UserRoleMapping { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
