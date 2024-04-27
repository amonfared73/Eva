using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.DTOs;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class Account : ModelBase
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public Account? Parent { get; set; }
        public IEnumerable<Account>? Accounts { get; set; }
        /// <summary>
        /// Implicit conversion for converting a <see cref="RootAccountDto"/> to an Account model
        /// This conversion is used to create a top level account with no parent
        /// </summary>
        /// <param name="accountDto"></param>
        public static implicit operator Account(RootAccountDto accountDto)
        {
            return new Account()
            {
                Name = accountDto.Name,
            };
        }
    }
}
