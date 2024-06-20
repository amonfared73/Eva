using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<IActionResult> CreatePermission(CreatePermissionViewModel permissionDto)
        {
            var result = await _permissionService.CreatePermission(permissionDto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IEnumerable<string>> GetUserPermissions(int userId)
        {
            return await _permissionService.GetUserPermissions(userId);
        }
    }
}
