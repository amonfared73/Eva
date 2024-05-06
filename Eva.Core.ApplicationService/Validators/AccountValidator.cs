using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Responses;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Validators
{
    public class AccountValidator
    {
        private bool _isValid;
        private List<string> _messages;
        public AccountValidator()
        {
            _isValid = true;
            _messages = new List<string>();
        }
        public AccountValidatorResponseViewModel Validate(EvaDbContext context)
        {
            _messages.Clear();

            if (context.Accounts.Any(a => a.ParentId == null))
                throw new EvaInvalidException("An account with null parent already exists! Please consider assigning a parent");

            return new AccountValidatorResponseViewModel()
            {
                IsValid = _isValid,
                ResponseMessage = new ResponseMessage(_messages)
            };
        }
    }
}
