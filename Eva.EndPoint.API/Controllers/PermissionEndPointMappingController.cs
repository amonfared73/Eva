using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    public class PermissionEndPointMappingController : EvaControllerBase<PermissionEndPointMapping, PermissionEndPointMappingViewModel>
    {
        private readonly IPermissionEndPointMappingService _permissionEndPointMappingService;
        public PermissionEndPointMappingController(IPermissionEndPointMappingService permissionEndPointMappingService) : base(permissionEndPointMappingService)
        {
            _permissionEndPointMappingService = permissionEndPointMappingService;
        }
    }
}
