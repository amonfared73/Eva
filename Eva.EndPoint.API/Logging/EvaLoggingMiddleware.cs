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
            await _logService.LogAsync(httpContext);
            await _next.Invoke(httpContext);
        }
    }
}
