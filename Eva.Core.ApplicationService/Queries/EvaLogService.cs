using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Enums;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Responses;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;
using Eva.Infra.Tools.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired(RegistrationType.Singleton)]
    public class EvaLogService : BaseService<EvaLog, EvaLogViewModel>, IEvaLogService
    {
        private readonly IEvaDbContextFactory _contextFactory;
        private readonly IUserService _userService;
        public EvaLogService(IEvaDbContextFactory contextFactory, IUserService userService) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _userService = userService;
        }
        public async Task LogAsync(HttpContext httpContext, string evaLogType, string requestBody, string responseBody)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                try
                {
                    var hasSensitiveCredentials = httpContext.IsLoginRequest() || httpContext.IsRegisterRequest();
                    var userId = await _userService.GetUserIdFromContext(httpContext, requestBody);
                    var loggedDate = DateTime.Now;

                    var evaLog = new EvaLog()
                    {
                        LogTypeCode = evaLogType,
                        RequestUrl = httpContext.Request.Path,
                        RequestMethod = httpContext.Request.Method,
                        StatusCode = httpContext.Response.StatusCode.ToString(),
                        Payload = hasSensitiveCredentials ? EvaLog.SensitiveCredentials : requestBody,
                        Response = hasSensitiveCredentials ? EvaLog.SensitiveCredentials : responseBody,
                        UserId = userId,
                        CreatedBy = userId,
                        CreatedOn = loggedDate,
                        ModifiedBy = userId,
                        ModifiedOn = loggedDate,
                    };
                    await context.EvaLogs.AddAsync(evaLog);
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {

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
        public async Task<PagedResultViewModel<EvaLogReportOutputViewModel>> EvaLogReportAsync(EvaLogReportInputViewModel request)
        {
            try
            {
                using (EvaDbContext context = _contextFactory.CreateDbContext())
                {
                    var users = await context.Users.Where(u => u.Id == request.UserId || request.UserId == null).ToListAsync();
                    var logs = await context.EvaLogs.ToListAsync();

                    var query = from log in logs
                                join user in users on log.CreatedBy equals user.Id
                                orderby log.CreatedOn descending
                                select new EvaLogReportOutputViewModel()
                                {
                                    Username = user.Username,
                                    CreatedOn = log.CreatedOn,
                                    RequestUrl = log.RequestUrl,
                                    RequestMethod = log.RequestMethod,
                                    Payload = log.Payload,
                                    Response = log.Response,
                                    StatusCode = log.StatusCode
                                };
                    var filteredQuery = query.ApplyBaseRequest(request, out Pagination pagination);

                    return new PagedResultViewModel<EvaLogReportOutputViewModel>()
                    {
                        Data = filteredQuery,
                        Pagination = pagination
                    };
                }
            }
            catch (EvaUnauthorizedException ex)
            {
                return new PagedResultViewModel<EvaLogReportOutputViewModel>()
                {
                    ResponseMessage = new ResponseMessage($"You do not have permission to this particular endpoint , {ex.Message}"),
                    HasError = true,
                };
            }
            catch (Exception ex)
            {
                return new PagedResultViewModel<EvaLogReportOutputViewModel>()
                {
                    ResponseMessage = new ResponseMessage($"Some error occurred , {ex.Message}"),
                    HasError = true,
                };
            }
        }

        // Eager loading practice
        public async Task<IEnumerable<SimpleUserLogReport>> SimpleUserLogReport()
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var logs = await context.EvaLogs
                    .Include(log => log.User)
                    .Select(log => new SimpleUserLogReport()
                    {
                        Username = log.User.Username,
                        RequestUrl = log.RequestUrl,
                        CreatedOn = log.CreatedOn
                    })
                    .ToListAsync();
                return logs;

            }
        }

        public async Task<ActionResultViewModel<EvaLog>> ClearAllLogsAsync()
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var logs = await context.EvaLogs.ToListAsync();
                context.EvaLogs.RemoveRange(logs);
                await context.SaveChangesAsync();
                return new ActionResultViewModel<EvaLog>()
                {
                    Entity = null,
                    HasError = false,
                    ResponseMessage = new ResponseMessage("All logs cleared")
                };
            }
        }
    }
}
