using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace Eva.EndPoint.API.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<AccessRequirement>
    {
        private readonly IUserContext _userContext;
        private readonly IPermissionEndPointMappingService _permissionEndPointMappingService;
        private readonly IRolePermissionMappingService _rolePermissionMappingService;
        private readonly IMemoryCache _memoryCache;
        private readonly AuthenticationConfiguration _authenticationConfiguration;
        private readonly EvaCachingKeys _keys;
        public PermissionAuthorizationHandler(IPermissionEndPointMappingService permissionEndPointMappingService, IMemoryCache memoryCache, AuthenticationConfiguration authenticationConfiguration, EvaCachingKeys keys, IUserContext userContext, IRolePermissionMappingService rolePermissionMappingService)
        {
            _permissionEndPointMappingService = permissionEndPointMappingService;
            _memoryCache = memoryCache;
            _authenticationConfiguration = authenticationConfiguration;
            _keys = keys;
            _userContext = userContext;
            _rolePermissionMappingService = rolePermissionMappingService;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessRequirement requirement)
        {
            // Get roles from JWT token
            var roles = context
                .User
                .Claims
                .Where(x => x.Type == CustomClaims.ActiveRoles)
                .Select(x => x.Value)
                .ToHashSet();

            // Set cache keys
            string permissionsCacheKey = $"{_keys.AccessiblePermissions} {_userContext.UserId}";
            string endPointsCacheKey = $"{_keys.AccessibleEndPoints} {_userContext.UserId}";

            // Declare valid permissions and endpoints
            HashSet<string> permissions, endPoints;

            // Grab permissions and endpoints from cache
            permissions = _memoryCache.Get<HashSet<string>>(permissionsCacheKey);
            endPoints = _memoryCache.Get<HashSet<string>>(endPointsCacheKey);

            // Set permissions in cache if null
            if (permissions is null)
            {
                permissions = await _rolePermissionMappingService.GetAccessiblePermissions(roles);
                _memoryCache.Set(permissionsCacheKey, permissions, TimeSpan.FromMinutes(_authenticationConfiguration.AccessTokenExpirationMinutes));
            }

            // Set endPoints in cache if null
            if (endPoints is null)
            {
                endPoints = await _permissionEndPointMappingService.GetAccessibleEndPoints(permissions);
                _memoryCache.Set(endPointsCacheKey, endPoints, TimeSpan.FromMinutes(_authenticationConfiguration.AccessTokenExpirationMinutes));
            }
            
            bool hasRole = roles.Contains(requirement.Access);
            bool hasPermission = permissions.Any(p => p.Contains(requirement.Access));
            bool hasEndPoint = endPoints.Any(e => e.Contains(requirement.Access));

            // True if the user has the required permission or the desired endpoint exists in at least of the user's permissions
            bool hasAccess = hasRole || hasPermission || hasEndPoint;

            if (hasAccess)
                context.Succeed(requirement);
        }
    }
}
