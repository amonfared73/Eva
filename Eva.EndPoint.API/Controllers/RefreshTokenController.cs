using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Responses;
using Eva.EndPoint.API.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpDelete]
        [HasRole(ActiveRoles.SystemDeveloper)]
        public async Task<ActionResultViewModel<RefreshToken>> ClearAllRefreshTokens()
        {
            try
            {
                return await _service.ClearAllRefreshTokens();
            }
            catch (Exception ex)
            {
                return new ActionResultViewModel<RefreshToken>()
                {
                    Entity = null,
                    HasError = true,
                    ResponseMessage = new ResponseMessage(ex.Message)
                };
            }
        }
    }
}
