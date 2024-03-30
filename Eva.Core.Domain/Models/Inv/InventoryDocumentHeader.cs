using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models.General;

namespace Eva.Core.Domain.Models.Inv
{
    [EvaEntity]
    public class InventoryDocumentHeader : ModelBase
    {
        public IEnumerable<InventoryDocumentDetail> InventoryDocumentDetails { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public int PartyId { get; set; }
        public Party Party { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
