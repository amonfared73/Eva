using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models.General
{
    [EvaEntity]
    public class MeasureUnit : ModelBase
    {
        public string Name { get; set; } = string.Empty;
        public override string ToString() => Name;
    }
}
