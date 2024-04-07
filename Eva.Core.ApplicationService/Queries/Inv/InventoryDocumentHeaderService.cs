using Eva.Core.ApplicationService.Services.Inv;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Models.Inv;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.ViewModels.Inv;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries.Inv
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class InventoryDocumentHeaderService : BaseService<InventoryDocumentHeader, InventoryDocumentHeaderViewModel>, IInventoryDocumentHeaderService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public InventoryDocumentHeaderService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
