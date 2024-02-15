using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class EvaLogService : IEvaLogService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        private readonly IUserService _userService;
        public EvaLogService(IDbContextFactory<EvaDbContext> contextFactory, IUserService userService)
        {
            _contextFactory = contextFactory;
            _userService = userService;
        }

        public async Task LogServiceAsync(HttpContext httpContext, string requestBody, string responseBody)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var evaLog = new EvaLog()
                {
                    LogTypeCode = EvaLogTypeCode.ServiceLog,
                    RequestUrl = httpContext.Request.Path,
                    RequestMethod = httpContext.Request.Method,
                    StatusCode = httpContext.Response.StatusCode.ToString(),
                    Payload = requestBody,
                    Response = responseBody,
                    UserId = await _userService.GetUserIdFromContext(httpContext, requestBody),
                    CreatedOn = DateTime.Now,
                };
                await context.EvaLogs.AddAsync(evaLog);
                await context.SaveChangesAsync();
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
