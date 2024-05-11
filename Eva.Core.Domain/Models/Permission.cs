using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.ViewModels;
using System.Text.Json.Serialization;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Permission : ModelBase
    {
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<RolePermissionMapping> RolePermissionMappings { get; set; }
        [JsonIgnore]
        public ICollection<PermissionEndPointMapping> PermissionEndPointMappings { get; set; }

        public static implicit operator Permission(CreatePermissionViewModel permissionDto)
        {
            return new Permission()
            {
                Name = permissionDto.Name,
            };
        }
    }
}
