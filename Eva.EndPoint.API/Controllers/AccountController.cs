using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;

namespace Eva.EndPoint.API.Controllers
{
    public class AccountController : EvaControllerBase<Account>
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) : base(accountService)
        {
            _accountService = accountService;
        }
    }
}
