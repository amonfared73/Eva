using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Models;

namespace Eva.Core.ApplicationService.Services
{
    public interface IUserRoleMappingService : IBaseService<UserRoleMapping>
    {
        Task<ActionResultViewModel<UserRoleMapping>> AddRoleToUserAsync(UserRoleMappingDto request);
    }
}
