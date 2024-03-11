using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;

namespace Eva.EndPoint.API.Controllers
{
    public class EvaEndpointController : EvaControllerBase<EvaEndPoint>
    {
        private readonly IEvaEndPointService _evaEndPoint;
        public EvaEndpointController(IEvaEndPointService evaEndPoint) : base(evaEndPoint)
        {
            _evaEndPoint = evaEndPoint;
        }
    }
}
