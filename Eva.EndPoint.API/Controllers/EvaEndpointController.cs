using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.EndPoint.API.Controllers
{
    public class EvaEndpointController : EvaControllerBase<EvaEndPoint, EvaEndPointViewModel>
    {
        private readonly IEvaEndPointService _evaEndPoint;
        public EvaEndpointController(IEvaEndPointService evaEndPoint) : base(evaEndPoint)
        {
            _evaEndPoint = evaEndPoint;
        }
    }
}
