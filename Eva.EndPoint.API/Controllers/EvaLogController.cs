using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Exceptions;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.Responses;
using Eva.Core.Domain.ViewModels;
using Eva.EndPoint.API.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    public class EvaLogController :EvaControllerBase<EvaLog>
    {
        private readonly IEvaLogService _logService;
        public EvaLogController(IEvaLogService logService) : base(logService)
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
            try
            {
                return await _logService.EvaLogReportAsync(request);
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

        [HttpPost]
        public async Task<IEnumerable<SimpleUserLogReport>> SimpleUserLogReport()
        {
            return await _logService.SimpleUserLogReport();
        }
    }
}
