using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IUserRoleMappingService : IBaseService<UserRoleMapping>
    {
        Task<ActionResultViewModel<UserRoleMapping>> AddRoleToUserAsync(UserRoleMappingDto request);
        Task<HashSet<string>> GetRolesForUserAsync(int userId);
        Task<IEnumerable<UserRolesViewModel>> UserRolesReport(int? userId);
    }
}
