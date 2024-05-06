using Eva.Core.Domain.Models;
using Eva.Core.Domain.Responses;
using Eva.Core.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.ApplicationService.Validators
{
    public class AccountValidator
    {
        private bool _isValid;
        private List<string> _messages;
        public AccountValidatorResponseViewModel Validate(Account user)
        {
            _messages.Clear();

            


            return new AccountValidatorResponseViewModel()
            {
                IsValid = _isValid,
                ResponseMessage = new ResponseMessage(_messages)
            };
        }
    }
}
