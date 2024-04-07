using Eva.Core.ApplicationService.Services.General;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Models.General;
using Eva.Core.Domain.ViewModels.General;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries.General
{
    [RegistrationRequired]
    public class GoodService : BaseService<Good, GoodViewModel>, IGoodService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public GoodService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
