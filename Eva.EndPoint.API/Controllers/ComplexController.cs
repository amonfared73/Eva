using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;

namespace Eva.EndPoint.API.Controllers
{
    public class ComplexController : EvaControllerBase<Complex>
    {
        private readonly IComplexService _service;

        public ComplexController(IComplexService service) : base(service) 
        {
            _service = service;
        }
    }
}
