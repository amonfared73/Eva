using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

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

        public async Task LogAsync(HttpContext httpContext, string requestBody)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var evaLog = new EvaLog()
                {
                    RequestUrl = httpContext.Request.Path,
                    RequestMethod = httpContext.Request.Method,
                    StatusCode = httpContext.Response.StatusCode.ToString(),
                    Payload = requestBody,
                    UserId = httpContext.Request.Path.Value == "/api/Authentication/Login" ? 1 : await _userService.ExtractUserIdFromToken(httpContext),
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
