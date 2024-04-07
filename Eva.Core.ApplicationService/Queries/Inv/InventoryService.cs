using Eva.Core.ApplicationService.Services.Inv;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.Models.Inv;
using Eva.Core.Domain.ViewModels.Inv;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries.Inv
{
    [RegistrationRequired]
    public class InventoryService : BaseService<Inventory, InventoryViewModel>, IInventoryService
    {
        private readonly IEvaDbContextFactory _dbContextFactory;
        public InventoryService(IEvaDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
