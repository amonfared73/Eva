using Eva.Core.ApplicationService.ExternalServices.OpenMeteo;
using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers
{
    [DisableBaseOperations]
    public class WeatherForcastController : EvaControllerBase<WeatherForcast>
    {
        private readonly IWeatherForcastService _service;
        private readonly IOpenMeteoService _openMeteoService;
        public WeatherForcastController(IWeatherForcastService service, IOpenMeteoService openMeteoService) : base(service)
        {
            _service = service;
            _openMeteoService = openMeteoService;
        }
        [HttpPost]
        public async Task<WeatherForcastResultViewModel> ForcastAsync(WeatherForcastViewModel reqeust)
        {
            return await _openMeteoService.ForcastAsync(reqeust);
        }
    }
}
