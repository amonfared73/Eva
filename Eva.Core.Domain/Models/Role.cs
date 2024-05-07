using Bogus;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using System.Text.Json.Serialization;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Role : ModelBase
    {
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<UserRoleMapping> UserRoleMappings { get; set; }
        [JsonIgnore]
        public ICollection<RolePermissionMapping> RolePermissionMappings { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public static Faker<Role> FakeRoleGenerator()
        {
            return new Faker<Role>()
                .RuleFor(r => r.Name, f => f.Name.JobTitle());
        }
    }
}
