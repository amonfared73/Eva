using Eva.Core.Domain.BaseInterfaces;
using Eva.Core.Domain.BaseModels;
using Microsoft.Extensions.Configuration;

namespace Eva.EndPoint.API.Extensions
{
    public static class IConfigurationExtensions
    {
        private static string CurrentDb = "sqlite";
        public static AuthenticationConfiguration GetEvaAuthenticationConfiguration(this IConfiguration configuration)
        {
            var authenticationConfiguration = new AuthenticationConfiguration();
            configuration.Bind("AuthenticationConfiguration", authenticationConfiguration);
            return authenticationConfiguration;
        }
        public static string GetEvaConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(CurrentDb);
        }
        public static IEnumerable<IEvaEntityConfiguration> GetEvaEntityConfigurations(this IConfiguration configuration)
        {
            var types = typeof(IEvaEntityConfiguration).Assembly.GetTypes().Where(t => typeof(IEvaEntityConfiguration).IsAssignableFrom(t) && t.IsClass);
            var instances = new List<IEvaEntityConfiguration>();
            foreach (var type in types)
            {
                var config = (IEvaEntityConfiguration)Activator.CreateInstance(type);
                configuration.Bind(type.Name, config);
                instances.Add(config);
            }
            return instances;
        }
        public static IConfiguration GetEvaConfigurations(this WebApplicationBuilder builder)
        {
            return builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        }
    }
}
