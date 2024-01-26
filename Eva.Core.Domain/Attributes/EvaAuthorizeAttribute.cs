using Microsoft.AspNetCore.Authorization;

namespace Eva.Core.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class EvaAuthorizeAttribute : AuthorizeAttribute
    {

    }
}
