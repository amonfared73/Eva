using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Eva.Infra.Tools.Extensions
{
    public static class IHttpContextAccessorExtensions
    {
        public static bool IsLoginRequest(this IHttpContextAccessor contextAccessor)
        {
            return contextAccessor.HttpContext.Request.Path.Value == Authentication.LoginUrl;
        }
        public static int GetUserId(this IHttpContextAccessor contextAccessor)
        {
            return contextAccessor.HttpContext.User.FindFirst(CustomClaims.UserId).Value.ToInt();
        }
        public static bool IsAuthenticated(this IHttpContextAccessor contextAccessor)
        {
            return contextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false;
        }
    }
}
