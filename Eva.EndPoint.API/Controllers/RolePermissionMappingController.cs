using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    public class RolePermissionMappingController : EvaControllerBase<RolePermissionMapping, RolePermissionMappingViewModel>
    {
        private readonly IRolePermissionMappingService _rolePermissionMappingService;
        public RolePermissionMappingController(IRolePermissionMappingService rolePermissionMappingService) : base(rolePermissionMappingService)
        {
            _rolePermissionMappingService = rolePermissionMappingService;
        }
    }
}
