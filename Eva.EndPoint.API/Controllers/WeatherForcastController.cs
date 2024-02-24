using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.EndPoint.API.Controllers
{
    public class WeatherForcastController : EvaControllerBase<WeatherForcast>
    {
        private readonly IWeatherForcastService _service;
        public WeatherForcastController(IWeatherForcastService service) : base(service)
        {
            _service = service;
        }
    }
}
