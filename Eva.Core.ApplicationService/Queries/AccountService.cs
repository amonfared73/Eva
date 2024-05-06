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

        public async Task<IEnumerable<AccountViewModel>> AccountGetAll()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var query = await context
                    .Accounts
                    .Where(a => a.Parent == null)
                    .Select(GetAccountsProjection(2, 0))
                    .OrderBy(a => a.Id)
                    .ToListAsync();

                return query;
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

        private static Expression<Func<Account, AccountViewModel>> GetAccountsProjection(int maxDepth, int currentDepth = 0)
        {
            currentDepth++;
            Expression<Func<Account, AccountViewModel>> result = account => new AccountViewModel()
            {
                ParentId = account.ParentId,
                Name = account.Name,
                ChildAccounts = currentDepth == maxDepth
                    ? new List<AccountViewModel>()
                    : account.Accounts.AsQueryable()
                    .Select(GetAccountsProjection(maxDepth, currentDepth))
                    .OrderBy(x => x.Id).ToList()
            };
            return result;
        }
    }
}
