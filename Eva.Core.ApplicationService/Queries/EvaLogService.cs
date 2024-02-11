using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class EvaLogService : BaseService<EvaLog>, IEvaLogService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        public EvaLogService(IDbContextFactory<EvaDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task LogAsync(HttpContext httpContext)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var evaLog = new EvaLog()
                {
                    RequestUrl = httpContext.Request.Path,
                    RequestMethod = httpContext.Request.Method,
                    StatusCode = httpContext.Response.StatusCode.ToString(),
                    Payload = httpContext.Request.Body.ToString(),
                    UserId = int.Parse(httpContext.User.Claims.Where(c => c.Type == "id").FirstOrDefault().ToString()),
                    CreatedOn = DateTime.Now,
                };
                await context.EvaLogs.AddAsync(evaLog);
                await context.SaveChangesAsync();
            }
        }
    }
}
