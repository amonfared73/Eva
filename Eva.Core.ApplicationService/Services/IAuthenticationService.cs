using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Services
{
    public interface IAuthenticationService : IBaseService<Authentication, AuthenticationViewModel>
    {
    }
}
