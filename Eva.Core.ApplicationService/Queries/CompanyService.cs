using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        public CompanyService(IDbContextFactory<EvaDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
