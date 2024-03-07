using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Eva.Infra.Tools.Extentions
{
    public static class IHttpContextAccessorExtensions
    {
        public static bool IsLoginRequeust(this IHttpContextAccessor contextAccessor)
        {
            return contextAccessor.HttpContext.Request.Path.Value == Authentication.LoginUrl;
        }
        public static int GetUserId(this IHttpContextAccessor contextAccessor)
        {
            return contextAccessor.HttpContext.User.FindFirst(CustomClaims.UserId).Value.ToInt();
        }
    }
}
