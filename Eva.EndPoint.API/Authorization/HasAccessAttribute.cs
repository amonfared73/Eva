using Microsoft.AspNetCore.Authorization;

namespace Eva.EndPoint.API.Authorization
{
    /// <summary>
    /// This attribute will apply policy based authorization, the policy could be the name of decorating endpoint, the permission or the role.
    /// Both controllers and Action methods can be decorated with <see cref="HasAccessAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class HasAccessAttribute : AuthorizeAttribute
    {
        public HasAccessAttribute(string policy) : base(policy)
        {
        }
    }
}
