using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Eva.Core.ApplicationService.Services
{
    public interface IEvaLogService
    {
        Task LogServiceAsync(HttpContext httpContext, string requestBody, string responseBody);
        Task<IEnumerable<EvaLog>> ViewAllLogsAsync();
        Task<IEnumerable<EvaLogReportViewModel>> EvaLogReportAsync(int? userId);
    }
}
