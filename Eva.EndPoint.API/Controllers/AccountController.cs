using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using System.Web.Http;

namespace Eva.EndPoint.API.Controllers
{
    public class AccountController : EvaControllerBase<Account, AccountViewModel>
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) : base(accountService)
        {
            _accountService = accountService;
        }
        [NonAction]
        public override Task<ActionResultViewModel<Account>> InsertAsync(Account entity)
        {
            return base.InsertAsync(entity);
        }
        [HttpPost]
        public async Task<ActionResultViewModel<Account>> CreateRootAccount(RootAccountDto accountDto)
        {
            return await _accountService.CreateRootAccount(accountDto);
        }
    }
}
