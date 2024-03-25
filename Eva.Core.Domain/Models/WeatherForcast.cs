using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    [EvaEntity]
    public class WeatherForcast : ModelBase
    {
        public static string WeatherForcastUrl = "https://api.open-meteo.com/v1/forecast";
    }
}
