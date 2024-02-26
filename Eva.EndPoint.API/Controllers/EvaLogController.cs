using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.EndPoint.API.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    [Authorize]
    [ApiController]
    [DisableBaseOperations]
    [Route("api/[controller]/[action]")]
    public class EvaLogController 
    {
        private readonly IEvaLogService _logService;
        public EvaLogController(IEvaLogService logService) 
        {
            _logService = logService;
        }
        [HttpGet]
        [HasRole(ActiveRoles.SystemDeveloper)]
        public async Task<IEnumerable<EvaLog>> ViewAllLogsAsync()
        {
            return await _logService.ViewAllLogsAsync();
        }
        [HttpPost]
        [HasRole(ActiveRoles.SystemDeveloper)]
        public async Task<PagedResultViewModel<EvaLogReportOutputViewModel>> EvaLogReportAsync(EvaLogReportInputViewModel request)
        {
            return await _logService.EvaLogReportAsync(request);
        }
    }
}
