using Eva.Core.ApplicationService.Services.General;
using Eva.Core.Domain.Models.General;
using Eva.Core.Domain.ViewModels.General;

namespace Eva.EndPoint.API.Controllers.General
{
    public class PartyController : EvaControllerBase<Party, PartyViewModel>
    {
        private readonly IPartyService _partyService;
        public PartyController(IPartyService partyService) : base(partyService)
        {
            _partyService = partyService;
        }
    }
}
