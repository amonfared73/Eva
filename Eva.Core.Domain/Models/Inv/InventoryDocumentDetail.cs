using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models.General;

namespace Eva.Core.Domain.Models.Inv
{
    [EvaEntity]
    public class InventoryDocumentDetail : ModelBase
    {
        public int GoodId { get; set; }
        public Good Good { get; set; }
        public int MeasureUnitId { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        public int InventoryDocumentHeaderId { get; set; }
        public InventoryDocumentHeader InventoryDocumentHeader { get; set; }
        public decimal IncomingAmount { get; set; }
        public decimal OutgoingAmount { get; set; }

    }
}
