using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;

namespace Eva.EndPoint.API.Controllers
{
    public class UserRoleMappingController : EvaControllerBase<UserRoleMapping>
    {
        private readonly IUserRoleMappingService _service;

        public UserRoleMappingController(IUserRoleMappingService service) : base(service)
        {
            _service = service;
        }
    }
}
