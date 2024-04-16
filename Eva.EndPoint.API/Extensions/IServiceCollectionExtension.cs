using Eva.EndPoint.API.Configurations;

namespace Eva.EndPoint.API.Extensions
{
    public static partial class IServiceCollectionExtension
    {
        /// <summary>
        /// Wraps all needed service registration in a single extension method
        /// </summary>
        /// <param name="services"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        public static IServiceCollection AddEvaServiceConfigurations(this IServiceCollection services, Action<EvaOptions> configuration)
        {
            var evaOptions = new EvaOptions();
            configuration?.Invoke(evaOptions);

            services
                .AddEvaAuthenticationConfiguration(evaOptions.EvaAuthenticationConfiguration)
                .AddEvaConfigurationEntities(evaOptions.EvaConfiguration)
                .AddEvaControllers(evaOptions.EvaConventions)
                .AddEvaAuthentication(evaOptions.EvaAuthenticationConfiguration)
                .AddEndpointsApiExplorer()
                .AddEvaSwagger()
                .AddEvaExternalServices()
                .AddHttpContextAccessor()
                .AddEvaUserContext()
                .AddEvaDbContext(evaOptions.EvaConnectionString)
                .AddEvaAccessTokenGenerator()
                .AddEvaEntityValidators()
                .AddEvaCryptographyServices()
                .AddEvaRoleBasedAuthorization()
                .AddEvaServices();

            return services;
        }
    }
}
