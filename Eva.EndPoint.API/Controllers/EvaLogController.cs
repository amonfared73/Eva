using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    public class EvaLogController : EvaControllerBase<EvaLog>
    {
        private readonly IEvaLogService _service;
        public EvaLogController(IEvaLogService service) : base(service)
        {
            _service = service;
        }
    }
}
