using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models.Inv
{
    [EvaEntity]
    public class Inventory : ModelBase
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public override string ToString() => $"{Number} {Name}";
    }
}
