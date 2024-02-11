using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
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
        private readonly IEvaLogService _service;
        public EvaLogController(IEvaLogService service) 
        {
            _service = service;
        }
    }
}
