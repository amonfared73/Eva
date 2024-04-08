using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Core.Domain.Enums;
using Eva.Infra.EntityFramework.DbContexts;

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
    }
}
