using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IPermissionEndPointMappingService : IEvaBaseService<PermissionEndPointMapping, PermissionEndPointMappingViewModel>
    {
        Task<HashSet<string>> GetAccessibleEndPoints(IEnumerable<string> permissions);
        Task<ActionResultViewModel<PermissionEndPointMapping>> AppendEndPointToPermission(PermissionEndpointMappingDto model);
    }
}
