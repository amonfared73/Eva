namespace Eva.EndPoint.API.Middlewares
{
    public class EvaExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public EvaExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
