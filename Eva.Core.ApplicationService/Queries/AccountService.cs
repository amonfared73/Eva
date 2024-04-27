using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Core.Domain.Enums;
using Eva.Infra.EntityFramework.DbContexts;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.DTOs;

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
        public async Task<ActionResultViewModel<Account>> CreateRootAccount(RootAccountDto accountDto)
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
