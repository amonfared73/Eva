using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class AuthenticationService : BaseService<Authentication, AuthenticationViewModel>, IAuthenticationService
    {
        private readonly IEvaDbContextFactory _contextFactory;
        public AuthenticationService(IEvaDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
