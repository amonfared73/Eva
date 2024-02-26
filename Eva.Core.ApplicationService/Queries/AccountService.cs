using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class AccountService : BaseService<Account>, IAccountService
    {
        private readonly IDbContextFactory<EvaDbContext> _dbContextFactory;
        public AccountService(IDbContextFactory<EvaDbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
