using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;

namespace Eva.Core.ApplicationService.Queries
{
    public class EvaEndPointService : BaseService<EvaEndPoint>, IEvaEndPointService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public EvaEndPointService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
