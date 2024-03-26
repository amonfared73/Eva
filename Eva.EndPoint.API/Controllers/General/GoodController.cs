using Eva.Core.ApplicationService.Services.General;
using Eva.Core.Domain.Models.General;
using Eva.Core.Domain.ViewModels.General;

namespace Eva.EndPoint.API.Controllers.General
{
    public class GoodController : EvaControllerBase<Good, GoodViewModel>
    {
        private readonly IGoodService _goodService;
        public GoodController(IGoodService goodService) : base(goodService)
        {
            _goodService = goodService;
        }
    }
}
