﻿using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    public class WeatherForcast : DomainObject
    {
        public static string WeatherForcastUrl = "https://api.open-meteo.com/v1/forecast";
    }
}