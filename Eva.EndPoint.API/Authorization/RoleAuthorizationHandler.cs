using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Eva.EndPoint.API.Authorization
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RoleAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var roles = context.User.Claims.Where(x => x.Type == CustomClaims.Role).Select(x => x.Value).ToHashSet();

            if (roles.Contains(requirement.Role))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
