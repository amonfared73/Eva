using Eva.Core.ApplicationService.Services;

namespace Eva.EndPoint.API.Middlewares
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

            var originalBodyStream = httpContext.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                httpContext.Response.Body = responseBody;

                await _next(httpContext);

                responseBody.Seek(0, SeekOrigin.Begin);
                var responseBodyContent = await new StreamReader(responseBody).ReadToEndAsync();
                await _logService.LogServiceAsync(httpContext, requestBody, responseBodyContent);

                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
