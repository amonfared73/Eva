using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContexts;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class WeatherForcastService : BaseService<WeatherForcast, WeatherForcastViewModel>, IWeatherForcastService
    {
        private readonly IEvaDbContextFactory _contextFactory;
        public WeatherForcastService(IEvaDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
