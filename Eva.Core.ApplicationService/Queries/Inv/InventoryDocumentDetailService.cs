using Eva.Core.ApplicationService.Services.Inv;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Models.Inv;
using Eva.Core.Domain.ViewModels.Inv;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries.Inv
{
    [RegistrationRequired]
    public class InventoryDocumentDetailService : BaseService<InventoryDocumentDetail, InventoryDocumentDetailViewModel>, IInventoryDocumentDetailService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public InventoryDocumentDetailService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
