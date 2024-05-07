using Microsoft.AspNetCore.Authorization;

namespace Eva.EndPoint.API.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Role { get; }

        public PermissionRequirement(string role)
        {
            Role = role;
        }
    }
}
