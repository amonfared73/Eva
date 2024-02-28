using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseModels;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Eva.EndPoint.API.Middlewares
{
    /// <summary>
    /// Middleware to log all exceptions caught through the Eva application
    /// </summary>
    public class EvaExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IEvaLogService _logService;

        public EvaExceptionMiddleware(RequestDelegate next, IEvaLogService logService)
        {
            _next = next;
            _logService = logService;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Request.EnableBuffering();
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
                httpContext.Request.Body.Position = 0;
                await _logService.LogAsync(httpContext, EvaLogTypeCode.ExceptionLog, requestBody, ex.Message);
            }
        }
    }
}
