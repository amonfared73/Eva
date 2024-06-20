using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;


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
        public override Task<PagedResultViewModel<Account>> GetAllAsync(BaseRequestViewModel request)
        {
            return base.GetAllAsync(request);
        }
        [NonAction]
        public override Task<ActionResultViewModel<Account>> InsertAsync(Account entity)
        {
            return base.InsertAsync(entity);
        }
        [NonAction]
        public override Task<SingleResultViewModel<Account>> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResultViewModel<Account>> CreateRootAccount(AccountDto accountDto)
        {
            try
            {
                return await _accountService.CreateRootAccount(accountDto);
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<Account>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
        }
        [HttpPost]
        public async Task<ActionResultViewModel<Account>> AppendAccount(AppendAccountViewModel model)
        {
            try
            {
                return await _accountService.AppendAccount(model);
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<Account>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new Core.Domain.Responses.ResponseMessage(ex.Message)
                };
            }
        }

        [HttpGet]
        public async Task<IActionResult> AccountGetById(int accountId)
        {
            try
            {
                var result = await _accountService.AccountGetById(accountId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
