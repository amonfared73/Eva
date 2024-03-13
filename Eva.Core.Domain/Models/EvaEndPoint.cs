using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    public class EvaEndPoint : DomainObject
    {
        public string Url { get; set; } = string.Empty;
    }
}
