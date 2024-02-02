using Eva.Core.ApplicationService.Services;
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
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            string? userId = context.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

            if(!int.TryParse(userId, out int parsedUserId))
            {
                return;
            }
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                IUserRoleMappingService userRoleMappingService = scope.ServiceProvider.GetRequiredService<IUserRoleMappingService>();
                var roles = await userRoleMappingService.GetRolesForUserAsync(parsedUserId);
                if (roles.Contains(requirement.Role))
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}
