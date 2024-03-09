using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    public class RefreshTokenController : EvaControllerBase<RefreshToken>
    {
        private readonly IRefreshTokenService _service;

        public RefreshTokenController(IRefreshTokenService service) : base(service)
        {
            _service = service;
        }
    }
}
