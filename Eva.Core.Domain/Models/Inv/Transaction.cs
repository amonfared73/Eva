using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Enums.Inv;

namespace Eva.Core.Domain.Models.Inv
{
    [EvaEntity]
    public class Transaction : ModelBase
    {
        public int Number { get; set; }
        public TransactionTypeCode TransactionTypeCode { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
