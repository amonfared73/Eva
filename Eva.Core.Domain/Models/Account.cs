using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.DTOs;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Account : ModelBase
    {
        public string Name { get; set; } = string.Empty;
        public int ParentId { get; set; }
        public Account Parent { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
        public static implicit operator Account(RootAccountDto accountDto)
        {
            return new Account()
            {
                Name = accountDto.Name,
            };
        }
    }
}
