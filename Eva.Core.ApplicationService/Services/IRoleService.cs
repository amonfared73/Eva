using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;

namespace Eva.Core.ApplicationService.Services
{
    public interface IRoleService : IBaseService<Role>
    {
        Task<ActionResultViewModel<Role>> CreateRole(string name);
    }
}
