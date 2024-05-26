using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.ViewModels;
using Flurl;
using Flurl.Http;

namespace Eva.Core.ApplicationService.ExternalServices.OpenMeteo
{
    [ExternalService(typeof(OpenMeteoService))]
    public class OpenMeteoService : IOpenMeteoService
    {
        private readonly HttpClient _httpClient;
        private readonly ExternalServicesUri _externalServicesUri;
        public OpenMeteoService(HttpClient httpClient, ExternalServicesUri externalServicesUri)
        {
            _httpClient = httpClient;
            _externalServicesUri = externalServicesUri;
        }
        public async Task<WeatherForcastResultViewModel> ForcastAsync(WeatherForcastInput reqeust)
        {
            try
            {
                var forecasts = await _externalServicesUri
                    .WeatherForcast
                    .AppendQueryParam(new
                    {
                        latitude = reqeust.Latitude,
                        longitude = reqeust.Longitude,
                        hourly = "temperature_2m"
                    })
                    .GetAsync()
                    .ReceiveJson<WeatherForcastResultViewModel>();

                return forecasts;
            } 
            catch (Exception ex)
            {
                return new WeatherForcastResultViewModel()
                {
                    HasError = true,
                    ResponseMessage = new Domain.Responses.ResponseMessage(ex.Message)
                };
            }
        }
    }
}
