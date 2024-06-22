using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class EvaEndPointService : EvaBaseService<EvaEndPoint, EvaEndPointViewModel>, IEvaEndPointService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public EvaEndPointService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<EvaEndPoint> GetEndPointByName(string currentEndpointName)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var endpoint = await context.EvaEndPoints.FirstOrDefaultAsync(e => e.Url == currentEndpointName);
                return endpoint;
            }
        }
    }
}
