using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    public class Role : DomainObject
    {
        public string Name { get; set; } = string.Empty;
        public override string ToString()
        {
            return Name;
        }
    }
}
