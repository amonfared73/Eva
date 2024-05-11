using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class PermissionEndPointMapping : ModelBase
    {
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
        public int EvaEndPointId { get; set; }
        public EvaEndPoint EvaEndPoint { get; set; }
    }
}
