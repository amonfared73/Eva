using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.Domain.DTOs
{
    public class UserRoleMappingDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
