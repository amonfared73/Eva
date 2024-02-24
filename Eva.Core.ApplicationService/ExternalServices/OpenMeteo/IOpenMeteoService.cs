using Eva.Core.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.ApplicationService.ExternalServices.OpenMeteo
{
    public interface IOpenMeteoService
    {
        Task<WeatherForcastResultViewModel> ForcastAsync(WeatherForcastViewModel reqeust);
    }
}
