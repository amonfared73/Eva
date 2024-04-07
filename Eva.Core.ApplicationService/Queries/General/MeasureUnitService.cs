using Eva.Core.ApplicationService.Services.General;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Models.General;
using Eva.Core.Domain.ViewModels.General;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries.General
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class MeasureUnitService : BaseService<MeasureUnit, MeasureUnitViewModel>, IMeasureUnitService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public MeasureUnitService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
