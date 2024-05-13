using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace Eva.EndPoint.API.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUserContext _userContext;
        private readonly IPermissionEndPointMappingService _permissionEndPointMappingService;
        private readonly IMemoryCache _memoryCache;
        private readonly AuthenticationConfiguration _authenticationConfiguration;
        private readonly EvaCachingKeys _keys;
        public PermissionAuthorizationHandler(IPermissionEndPointMappingService permissionEndPointMappingService, IMemoryCache memoryCache, AuthenticationConfiguration authenticationConfiguration, EvaCachingKeys keys, IUserContext userContext)
        {
            _permissionEndPointMappingService = permissionEndPointMappingService;
            _memoryCache = memoryCache;
            _authenticationConfiguration = authenticationConfiguration;
            _keys = keys;
            _userContext = userContext;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // Get permissions from JWT token
            var permissions = context
                .User
                .Claims
                .Where(x => x.Type == CustomClaims.ActivePermissions)
                .Select(x => x.Value)
                .ToHashSet();

            string desiredCacheKey = $"{_keys.AccessibleEndPoints} {_userContext.UserId}";

            // Call accessible endpoints service
            HashSet<string> accessibleEndPoints;
            accessibleEndPoints = _memoryCache.Get<HashSet<string>>(desiredCacheKey);

            if (accessibleEndPoints is null)
            {
                accessibleEndPoints = await _permissionEndPointMappingService.GetAccessibleEndPoints(permissions);
                _memoryCache.Set(desiredCacheKey, accessibleEndPoints, TimeSpan.FromMinutes(_authenticationConfiguration.AccessTokenExpirationMinutes));
            }

            bool hasAccess = accessibleEndPoints.Any(uri => uri.Contains(requirement.Permission)) || permissions.Contains(requirement.Permission);

            if (hasAccess)
                context.Succeed(requirement);
        }
    }
}
