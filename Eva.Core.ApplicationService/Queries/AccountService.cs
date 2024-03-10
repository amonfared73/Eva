using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class AccountService : BaseService<Account>, IAccountService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public AccountService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
