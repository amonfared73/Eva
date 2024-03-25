using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class UserRoleMapping : ModelBase
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
