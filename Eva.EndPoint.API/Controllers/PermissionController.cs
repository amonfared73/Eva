using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    public class PermissionController : EvaControllerBase<Permission, PermissionViewModel>
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService) : base(permissionService) 
        {
            _permissionService = permissionService;
        }
    }
}
