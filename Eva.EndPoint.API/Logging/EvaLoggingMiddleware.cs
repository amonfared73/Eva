using Eva.Core.ApplicationService.Services;

namespace Eva.EndPoint.API.Logging
{
    public class EvaLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IEvaLogService _logService;

        public EvaLoggingMiddleware(RequestDelegate next, IEvaLogService logService)
        {
            _next = next;
            _logService = logService;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            var requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
            httpContext.Request.Body.Position = 0;
            await _logService.LogAsync(httpContext, requestBody);
            await _next(httpContext);
        }
    }
}
