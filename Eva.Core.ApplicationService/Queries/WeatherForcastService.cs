using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;

namespace Eva.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class WeatherForcastService : BaseService<WeatherForcast>, IWeatherForcastService
    {
        private readonly IDbContextFactory<EvaDbContext> _contextFactory;
        public WeatherForcastService(IDbContextFactory<EvaDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
