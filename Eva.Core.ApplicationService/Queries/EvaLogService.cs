using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [DisableBaseOperations]
    public class EvaLogService : BaseService<EvaLog>, IEvaLogService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        public EvaLogService(IDbContextFactory<EvaDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
