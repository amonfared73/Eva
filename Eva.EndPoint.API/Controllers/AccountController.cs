using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.EndPoint.API.Controllers
{
    public class AccountController : EvaControllerBase<Account, AccountViewModel>
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) : base(accountService)
        {
            _accountService = accountService;
        }
    }
}
