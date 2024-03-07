using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;

namespace Eva.EndPoint.API.Controllers
{
    public class IntrumentController : EvaControllerBase<Instrument>
    {
        private readonly IInstrumentService _instrumentService;
        public IntrumentController(IInstrumentService instrumentService) : base(instrumentService)
        {
            _instrumentService = instrumentService;
        }
    }
}
