using Eva.Core.Domain.BaseInterfaces;
using Eva.Core.Domain.BaseModels;

namespace Eva.EndPoint.API.Extensions
{
    public static class IConfigurationExtensions
    {
        private static string CurrentDb = "sqlite";
        /// <summary>
        /// Get <see href="https://github.com/amonfared73/Eva">Eva</see> Authentication Configuration
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static AuthenticationConfiguration GetEvaAuthenticationConfiguration(this IConfiguration configuration)
        {
            var authenticationConfiguration = new AuthenticationConfiguration();
            configuration.Bind("AuthenticationConfiguration", authenticationConfiguration);
            return authenticationConfiguration;
        }
        /// <summary>
        /// Get <see href="https://github.com/amonfared73/Eva">Eva</see> Connection string
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns><see cref="AuthenticationConfiguration"/></returns>
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
        /// <summary>
        /// Accessing an <see cref="IConfiguration"/> instance to reach <see href="https://github.com/amonfared73/Eva">Eva</see> appsettings.json
        /// </summary>
        /// <param name="builder"></param>
        /// <returns><see cref="IConfiguration"/></returns>
        public static IConfiguration GetEvaConfigurations(this WebApplicationBuilder builder)
        {
            return builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        }
    }
}
