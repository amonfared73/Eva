using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IAccountService : IBaseService<Account, AccountViewModel>
    {
        Task<ActionResultViewModel<Account>> CreateRootAccount(AccountDto accountDto);
        Task<ActionResultViewModel<Account>> AppendAccount(AppendAccountViewModel model);
    }
}
