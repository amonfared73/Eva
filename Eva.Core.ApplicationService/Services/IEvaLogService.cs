﻿using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Eva.Core.ApplicationService.Services
{
    public interface IEvaLogService
    {
        Task LogAsync(HttpContext httpContext, string evaLogType, string requestBody, string responseBody);
        Task<IEnumerable<EvaLog>> ViewAllLogsAsync();
        Task<IEnumerable<EvaLogReportOutputViewModel>> EvaLogReportAsync(int? userId);
    }
}
