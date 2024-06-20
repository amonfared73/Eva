using Microsoft.AspNetCore.Authorization;

namespace Eva.EndPoint.API.Authorization
{
    public class AccessRequirement : IAuthorizationRequirement
    {
        public string Access { get; }

        public AccessRequirement(string access)
        {
            Access = access;
        }
    }
}
