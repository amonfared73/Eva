using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models.General;

namespace Eva.Core.Domain.Models.Inv
{
    [EvaEntity]
    public class InventoryDocumentHeader : ModelBase
    {
        public int InventoryDocumentDetailId { get; set; }
        public IEnumerable<InventoryDocumentDetail> InventoryDocumentDetails { get; set; }
        public Transaction Transaction { get; set; }
        public DateTime EffectiveDate { get; set; }
        public Inventory Inventory { get; set; }
        public Party Party { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
