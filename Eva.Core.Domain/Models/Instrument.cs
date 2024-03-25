using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Instrument : ModelBase
    {
        public string Name { get; set; } = string.Empty;
        public int YearInvented { get; set; }
    }
}
