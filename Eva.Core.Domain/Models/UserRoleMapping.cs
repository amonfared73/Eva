using Eva.Core.Domain.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eva.Core.Domain.Models
{
    public class UserRoleMapping : DomainObject
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
