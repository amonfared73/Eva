﻿using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Responses;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContextes;
using Eva.Infra.Tools.Extentions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Eva.Core.Domain.Exceptions;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class EvaLogService : BaseService<EvaLog>, IEvaLogService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        private readonly IUserService _userService;
        public EvaLogService(IDbContextFactory<EvaDbContext> contextFactory, IUserService userService) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _userService = userService;
        }
        public async Task LogAsync(HttpContext httpContext, string evaLogType, string requestBody, string responseBody)
        {
            using (EvaDbContext context = _contextFactory.CreateDbContext())
            {
                var evaLog = new EvaLog()
                {
                    LogTypeCode = evaLogType,
                    RequestUrl = httpContext.Request.Path,
                    RequestMethod = httpContext.Request.Method,
                    StatusCode = httpContext.Response.StatusCode.ToString(),
                    Payload = httpContext.IsLoginRequest() ? "Sensitive Credentials" : requestBody,
                    Response = httpContext.IsLoginRequest() ? "Sensitive Credentials" : responseBody,
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
        public async Task<PagedResultViewModel<EvaLogReportOutputViewModel>> EvaLogReportAsync(EvaLogReportInputViewModel request)
        {
            try
            {
                using (EvaDbContext context = _contextFactory.CreateDbContext())
                {
                    var users = await context.Users.Where(u => u.Id == request.UserId || request.UserId == null).ToListAsync();
                    var logs = await context.EvaLogs.ToListAsync();

                    var query = from log in logs
                                join user in users on log.UserId equals user.Id
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
                    var filteredQuery = query.ApplyBaseRequest(request);
                    var totalRecords = query.Count();

                    return new PagedResultViewModel<EvaLogReportOutputViewModel>()
                    {
                        Data = filteredQuery,
                        Pagination = request.PaginationRequest.ToPagination(totalRecords)
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
                    ResponseMessage = new ResponseMessage($"Some error occured , {ex.Message}"),
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
