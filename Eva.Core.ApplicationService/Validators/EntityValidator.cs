﻿using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Responses;

namespace Eva.Core.ApplicationService.Validators
{
    public abstract class EntityValidator<T> where T : ModelBase
    {
        public bool _isValid;
        public List<string> _messages;
        public EntityValidator()
        {
            _isValid = true;
            _messages = new List<string>();
        }
        public virtual ValidationResponse Validate(T entity)
        {
            // Implement validation here

            return new ValidationResponse()
            {
                IsValid = _isValid,
                ResponseMessage = new ResponseMessage(_messages)
            };
        }
    }
}
