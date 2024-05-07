using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<IActionResult> AppendPermissionToRole(AppendPermissionToRoleViewModel appendDto)
        {
            try
            {
                var result = await _rolePermissionMappingService.AppendPermissionToRole(appendDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IEnumerable<string>> GetUserPermissions(int userId)
        {
            return await _rolePermissionMappingService.GetUserPermissions(userId);
        }
    }
}
