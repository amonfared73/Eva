using Eva.Core.ApplicationService.Services.Inv;
using Eva.Core.Domain.Models.Inv;
using Eva.Core.Domain.ViewModels.Inv;

namespace Eva.EndPoint.API.Controllers.Inv
{
    public class InventoryDocumentHeaderController : EvaControllerBase<InventoryDocumentHeader, InventoryDocumentHeaderViewModel>
    {
        private readonly IInventoryDocumentHeaderService _inventoryDocumentHeaderService;
        public InventoryDocumentHeaderController(IInventoryDocumentHeaderService inventoryDocumentHeaderService) : base(inventoryDocumentHeaderService)
        {
            _inventoryDocumentHeaderService = inventoryDocumentHeaderService;
        }
    }
}
