using Microsoft.AspNetCore.Authorization;

namespace Eva.EndPoint.API.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string policy) : base(policy)
        {
        }
    }
}
