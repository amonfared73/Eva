using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class InstrumentService : BaseService<Instrument>, IInstrumentService
    {
        private readonly IEvaDbContextFactory _contextFactory;
        public InstrumentService(IEvaDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
