using Eva.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace Eva.Core.ApplicationService.Services
{
    public interface IEvaLogService : IBaseService<EvaLog>
    {
        Task LogAsync(HttpContext httpContext);
    }
}
