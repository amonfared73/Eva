using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Account : ModelBase
    {
        public string Name { get; set; } = string.Empty;
        public int ParentId { get; set; }
        public Account Parent { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
