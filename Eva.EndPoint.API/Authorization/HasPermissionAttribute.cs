using Microsoft.AspNetCore.Authorization;

namespace Eva.EndPoint.API.Authorization
{
    /// <summary>
    /// This attribute will apply policy based authorization, the policy could be the name of decorating endpoint or the permission
    /// Both controllers and Action methods can be decorated with <see cref="HasPermissionAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string policy) : base(policy)
        {
        }
    }
}
