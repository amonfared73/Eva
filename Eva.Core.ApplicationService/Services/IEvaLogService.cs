﻿using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Eva.Core.ApplicationService.Services
{
    public interface IEvaLogService : IBaseService<EvaLog, EvaLogViewModel>
    {
        Task LogAsync(HttpContext httpContext, string evaLogType, string requestBody, string responseBody);
        Task<IEnumerable<EvaLog>> ViewAllLogsAsync();
        Task<PagedResultViewModel<EvaLogReportOutputViewModel>> EvaLogReportAsync(EvaLogReportInputViewModel request);
        Task<IEnumerable<SimpleUserLogReport>> SimpleUserLogReport();
        Task<ActionResultViewModel<EvaLog>> ClearAllLogsAsync();
    }
}
