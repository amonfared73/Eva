using Eva.Core.ApplicationService.Services.Inv;
using Eva.Core.Domain.Models.Inv;
using Eva.Core.Domain.ViewModels.Inv;

namespace Eva.EndPoint.API.Controllers.Inv
{
    public class InventoryDocumentDetailController : EvaControllerBase<InventoryDocumentDetail, InventoryDocumentDetailViewModel>
    {
        private readonly IInventoryDocumentDetailService _inventoryDocumentDetailService;
        public InventoryDocumentDetailController(IInventoryDocumentDetailService inventoryDocumentDetailService) : base(inventoryDocumentDetailService)
        {
            _inventoryDocumentDetailService = inventoryDocumentDetailService;
        }
    }
}
