using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Department : ModelBase
    {
        public string Name { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
