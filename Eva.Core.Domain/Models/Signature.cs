using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    public class Signature : DomainObject
    {
        public string Value { get; set; } = string.Empty;
    }
}
