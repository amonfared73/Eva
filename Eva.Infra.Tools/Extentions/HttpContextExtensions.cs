using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Eva.Infra.Tools.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool IsLoginRequest(this HttpContext httpContext)
        {
            return httpContext.Request.Path.Value == Authentication.LoginUrl;
        }
        public static bool IsRegisterRequest(this HttpContext httpContext)
        {
            return httpContext.Request.Path.Value == Authentication.RegisterUrl;
        }
    }
}
