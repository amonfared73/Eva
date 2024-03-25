using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Company : ModelBase
    {
        public string Name { get; set; } = string.Empty;
        public List<Department>? Departments { get; set; }
    }
}
