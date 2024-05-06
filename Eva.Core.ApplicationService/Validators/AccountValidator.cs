using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Responses;

namespace Eva.Core.ApplicationService.Validators
{
    public class AccountValidator : EntityValidator<Account>
    {
        public ValidationResponse Validate(IQueryable<Account> accounts)
        {
            _messages.Clear();

            if (accounts.Any(a => a.ParentId == null))
                throw new EvaInvalidException("An account with null parent already exists! Please consider assigning a parent");

            return new ValidationResponse()
            {
                IsValid = _isValid,
                ResponseMessage = new ResponseMessage(_messages)
            };
        }
    }
}
