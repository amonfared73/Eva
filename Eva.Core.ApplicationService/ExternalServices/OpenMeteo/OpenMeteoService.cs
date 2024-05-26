using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System;

namespace Eva.Core.ApplicationService.ExternalServices.OpenMeteo
{
    [ExternalService(typeof(OpenMeteoService))]
    public class OpenMeteoService : IOpenMeteoService
    {
        private readonly HttpClient _httpClient;
        public OpenMeteoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<WeatherForcastResultViewModel> ForcastAsync(WeatherForcastInput reqeust)
        {
            var url = $"{WeatherForcast.WeatherForcastUrl}?latitude={reqeust.Latitude.ToString()}&longitude={reqeust.Longitude.ToString()}&hourly=temperature_2m";

            try
            {
                var forecasts = await "https://api.open-meteo.com/v1/forecast"
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
