using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Core.Domain.Responses;

namespace Eva.Core.ApplicationService.Validators
{
    public class UserValidator
    {
        public UserValidatorResponseViewModel Validate(User user)
        {
            bool isValid = true;
            var messages = new List<string>();
            if (!user.IsAdmin)
            {
                isValid = false;
                messages.Add("User is not an admin");
            }

            if (string.IsNullOrEmpty(user.Signature))
            {
                isValid = false;
                messages.Add("User's signature is empty");
            }

            return new UserValidatorResponseViewModel()
            {
                State = isValid,
                ResponseMessage = new ResponseMessage(messages)
            };
        }
    }
}
