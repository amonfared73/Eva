using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class EvaEndPoint : ModelBase
    {
        public string Url { get; set; } = string.Empty;
        public ICollection<PermissionEndPointMapping> PermissionEndPointMappings { get; set; }
    }
}
