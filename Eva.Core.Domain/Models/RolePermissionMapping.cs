using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class RolePermissionMapping : ModelBase
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

        public static implicit operator RolePermissionMapping(AppendPermissionToRoleViewModel appendModel)
        {
            return new RolePermissionMapping()
            {
                RoleId = appendModel.RoleId,
                PermissionId = appendModel.PermissionId,
            };
        }
    }
}
