using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IRolePermissionMappingService : IEvaBaseService<RolePermissionMapping, RolePermissionMappingViewModel>
    {
        Task<CustomResultViewModel<RoleViewModel>> AppendPermissionToRole(AppendPermissionToRoleViewModel model);
        Task<HashSet<string>> GetUserPermissions(int userId);
        Task<HashSet<string>> GetAccessiblePermissions(IEnumerable<string> roles);
    }
}
    