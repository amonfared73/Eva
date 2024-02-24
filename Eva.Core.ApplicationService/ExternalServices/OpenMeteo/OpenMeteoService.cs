using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Newtonsoft.Json;
using System;

namespace Eva.Core.ApplicationService.ExternalServices.OpenMeteo
{
    public class OpenMeteoService : IOpenMeteoService
    {
        private readonly HttpClient _httpClient;
        public OpenMeteoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<WeatherForcastResultViewModel> ForcastAsync(WeatherForcastViewModel reqeust)
        {
            var url = string.Format("{0}?latitude={1}&longitude={2}&hourly=temperature_2m", WeatherForcast.WeatherForcastUrl, reqeust.Latitude.ToString(), reqeust.Longitude.ToString());
            var response = await _httpClient.GetAsync(url);

            try
            {
                var json = await response.Content.ReadAsStringAsync();
                var forecasts = JsonConvert.DeserializeObject<WeatherForcastResultViewModel>(json);
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
