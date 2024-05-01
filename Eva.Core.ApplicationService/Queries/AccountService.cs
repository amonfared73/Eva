using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Core.Domain.Enums;
using Eva.Infra.EntityFramework.DbContexts;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Eva.Core.Domain.Exceptions;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class AccountService : BaseService<Account, AccountViewModel>, IAccountService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public AccountService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<ActionResultViewModel<Account>> AppendAccount(AppendAccountViewModel model)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var parentAccount = await context.Accounts.FirstOrDefaultAsync(a => a.Id == model.ParentAccountId);
                if (parentAccount == null)
                    throw new EvaNotFoundException("Parent account not found", typeof(Account));

                var account = new Account()
                {
                    Name = model.Name,
                    ParentId = model.ParentAccountId,
                };
                await context.Accounts.AddAsync(account);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<Account>()
                {
                    Entity = account,
                    HasError = false,
                    ResponseMessage = new Domain.Responses.ResponseMessage("Account successfully appended")
                };
            }
        }

        public async Task<ActionResultViewModel<Account>> CreateRootAccount(AccountDto accountDto)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                Account acc = accountDto;
                await context.Accounts.AddAsync(acc);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<Account>()
                {
                    Entity = acc,
                    HasError = false,
                    ResponseMessage = new Domain.Responses.ResponseMessage("Account created successfully")
                };
            }
        }
    }
}
