using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using System.Text.Json.Serialization;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Permission : ModelBase
    {
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<RolePermissionMapping> RolePermissionMappings { get; set; }
    }
}
