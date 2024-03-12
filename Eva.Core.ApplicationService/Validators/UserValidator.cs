using Eva.Core.Domain.Models;

namespace Eva.Core.ApplicationService.Validators
{
    public class UserValidator
    {
        public bool Validate(User user)
        {
            if (!user.IsAdmin)
                return false;
            
            if(user.Signature is null)
                return false;

            return true;
        }
    }
}
