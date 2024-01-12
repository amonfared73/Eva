using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Microsoft.AspNetCore.Authorization;

namespace Eva.EndPoint.API.Controllers
{
    [Authorize]
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
