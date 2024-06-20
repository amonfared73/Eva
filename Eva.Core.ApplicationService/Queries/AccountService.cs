using Eva.Core.ApplicationService.Services;
using Eva.Core.ApplicationService.Validators;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class AccountService : BaseService<Account, AccountViewModel>, IAccountService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        private readonly AccountValidator _validator;
        public AccountService(IEvaDbContextFactory dbContextFactory, AccountValidator validator) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _validator = validator;
        }

        public async Task<Account> AccountGetById(int accountId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var account = await context
                    .Accounts
                    .Include(a => a.Accounts)
                    .FirstOrDefaultAsync(a => a.Id == accountId);
                await LoadChildAccounts(account);

                return account;
            }
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

                _validator.Validate(context.Accounts);

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

        private async Task LoadChildAccounts(Account account)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var childAccounts = await context
                    .Accounts
                    .Where(a => a.ParentId == account.Id)
                    .Include(a => a.Accounts)
                    .ToListAsync();

                account.Accounts = childAccounts;

                foreach (var childAccount in childAccounts)
                {
                    await LoadChildAccounts(childAccount);
                }
            }
        }
    }
}
