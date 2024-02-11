using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class EvaLogService : IEvaLogService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        public EvaLogService(IDbContextFactory<EvaDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task LogAsync(HttpContext httpContext)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                using (StreamReader streamReader = new StreamReader(httpContext.Request.Body, Encoding.UTF8))
                {
                    var evaLog = new EvaLog()
                    {
                        RequestUrl = httpContext.Request.Path,
                        RequestMethod = httpContext.Request.Method,
                        StatusCode = httpContext.Response.StatusCode.ToString(),
                        Payload = "Payload",
                        UserId = 1,
                        CreatedOn = DateTime.Now,
                    };
                    await context.EvaLogs.AddAsync(evaLog);
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task<IEnumerable<EvaLog>> ViewAllLogsAsync()
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.EvaLogs.ToListAsync();
            }
        }
    }
}
