﻿namespace Eva.Core.Domain.ViewModels
{
    public class WeatherForcastInput
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string hourly { get; set; } = "temperature_2m";
    }
}
