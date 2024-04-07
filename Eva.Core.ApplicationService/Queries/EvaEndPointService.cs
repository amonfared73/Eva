using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class EvaEndPointService : BaseService<EvaEndPoint, EvaEndPointViewModel>, IEvaEndPointService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public EvaEndPointService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
