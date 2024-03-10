using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    public class Instrument : DomainObject
    {
        public string Name { get; set; } = string.Empty;
        public int YearInvented { get; set; }
    }
}
