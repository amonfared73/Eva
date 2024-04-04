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
    }
}
