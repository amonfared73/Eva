using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<ActionResultViewModel<PermissionEndPointMapping>> AppendEndPointToPermission(PermissionEndpointMappingDto model)
        {
            try
            {
                return await _permissionEndPointMappingService.AppendEndPointToPermission(model);
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<PermissionEndPointMapping>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
        }
    }
}
