﻿using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Core.Domain.Responses;

namespace Eva.Core.ApplicationService.Validators
{
    public class UserValidator
    {
        private bool _isValid;
        private List<string> _messages;
        public UserValidator()
        {
            _isValid = true;
            _messages = new List<string>();
        }
        public UserValidatorResponseViewModel Validate(User user)
        {
            _messages.Clear();

            if (!user.IsAdmin)
            {
                _isValid = false;
                _messages.Add("User is not an admin");
            }

            if (string.IsNullOrEmpty(user.Signature))
            {
                _isValid = false;
                _messages.Add("User's signature is empty");
            }

            return new UserValidatorResponseViewModel()
            {
                IsValid = _isValid,
                ResponseMessage = new ResponseMessage(_messages)
            };
        }
    }
}
