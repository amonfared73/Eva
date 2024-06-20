using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IPermissionService : IBaseService<Permission, PermissionViewModel>
    {
        Task AppendPermission(string permission);
        Task<CustomResultViewModel<PermissionViewModel>> CreatePermission(CreatePermissionViewModel model);
        Task<IQueryable<string>> GetUserPermissions(int userId);
    }
}
