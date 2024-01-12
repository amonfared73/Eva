using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        public DepartmentService(IDbContextFactory<EvaDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
