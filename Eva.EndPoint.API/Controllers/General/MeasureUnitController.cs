using Eva.Core.ApplicationService.Services.General;
using Eva.Core.Domain.Models.General;
using Eva.Core.Domain.ViewModels.General;

namespace Eva.EndPoint.API.Controllers.General
{
    public class MeasureUnitController : EvaControllerBase<MeasureUnit, MeasureUnitViewModel>
    {
        private readonly IMeasureUnitService _measureUnitService;
        public MeasureUnitController(IMeasureUnitService measureUnitService) : base(measureUnitService)
        {
            _measureUnitService = measureUnitService;
        }
    }
}
