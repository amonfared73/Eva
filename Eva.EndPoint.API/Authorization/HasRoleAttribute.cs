using Microsoft.AspNetCore.Authorization;

namespace Eva.EndPoint.API.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class HasRoleAttribute : AuthorizeAttribute
    {
        public HasRoleAttribute(string policy) : base(policy)
        {
        }
    }
}
