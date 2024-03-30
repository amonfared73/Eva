using Eva.Core.ApplicationService.Services.Inv;
using Eva.Core.Domain.Models.Inv;
using Eva.Core.Domain.ViewModels.Inv;

namespace Eva.EndPoint.API.Controllers.Inv
{
    public class InventoryController : EvaControllerBase<Inventory, InventoryViewModel>
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService) : base(inventoryService)
        {
            _inventoryService = inventoryService;
        }
    }
}
