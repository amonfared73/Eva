using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IPermissionService : IEvaBaseService<Permission, PermissionViewModel>
    {
        Task<CustomResultViewModel<PermissionViewModel>> CreatePermission(CreatePermissionViewModel model);
    }
}
