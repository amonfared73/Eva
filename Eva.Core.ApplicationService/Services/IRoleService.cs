using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IRoleService : IBaseService<Role, RoleViewModel>
    {
        Task<ActionResultViewModel<Role>> CreateRole(string name);
    }
}
