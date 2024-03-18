using Eva.Core.ApplicationService.Services;
using Eva.Infra.Tools.Extentions;
using Microsoft.AspNetCore.Http;

namespace Eva.Core.ApplicationService.Queries
{
    public sealed class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsAuthenticated => _httpContextAccessor.IsAuthenticated();
        public int UserId => _httpContextAccessor.GetUserId();
    }
}
