using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
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
        public async Task<IEnumerable<EvaLog>> ViewAllLogsAsync()
        {
            return await _logService.ViewAllLogsAsync();
        }
        [HttpGet]
        public async Task<IEnumerable<EvaLogReportOutputViewModel>> EvaLogReportAsync(int? userId)
        {
            return await _logService.EvaLogReportAsync(userId);
        }
    }
}
