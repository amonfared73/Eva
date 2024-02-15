using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace Eva.Core.ApplicationService.Services
{
    public interface IEvaLogService
    {
        Task LogServiceAsync(HttpContext httpContext, string requestBody, string responseBody);
        Task<IEnumerable<EvaLog>> ViewAllLogsAsync();
    }
}
