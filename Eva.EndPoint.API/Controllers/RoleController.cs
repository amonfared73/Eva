using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;

namespace Eva.EndPoint.API.Controllers
{
    public class RoleController : EvaControllerBase<Role>
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service) : base(service)
        {
            _service = service;
        }
    }
}
