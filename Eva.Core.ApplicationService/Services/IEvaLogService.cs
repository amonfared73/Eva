using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace Eva.Core.ApplicationService.Services
{
    public interface IEvaLogService
    {
        Task LogRequestAsync(HttpContext httpContext, string requestBody);
        Task<IEnumerable<EvaLog>> ViewAllLogsAsync();
    }
}
