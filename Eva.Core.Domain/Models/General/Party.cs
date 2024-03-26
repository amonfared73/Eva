using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models.General
{
    [EvaEntity]
    public class Party : ModelBase
    {
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public override string ToString() => $"{Number} {Name}";
    }
}
