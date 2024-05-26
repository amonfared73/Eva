using Eva.Core.Domain.BaseModels;
using Eva.EndPoint.API.Conventions;
using Eva.EndPoint.API.Middlewares;

namespace Eva.EndPoint.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Creates an <see href="https://github.com/amonfared73/Eva">Eva</see> framework <see cref="WebApplicationBuilder"/> instance
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplicationBuilder AddEva(this WebApplicationBuilder builder, out WebApplication app)
        {
            // Accessing an IConfiguration instance to reach appsettings.json
            var configuration = builder.GetEvaConfigurations();

            // Get Connection string
            var connectionString = configuration.GetEvaConnectionString();

            // Get caching keys
            var cachingKeys = configuration.GetEvaCachingKeys();

            // Get Eva Authentication Configuration
            var authenticationConfiguration = configuration.GetEvaAuthenticationConfiguration();

            // Get external API uris
            var evaExternalApis = configuration.GetEvaExternalApiUris();

            // Add Eva required services
            builder.Services.AddEvaServiceConfigurations(evaOptions =>
            {
                evaOptions.EvaConfiguration = configuration;
                evaOptions.EvaConnectionString = connectionString;
                evaOptions.EvaAuthenticationConfiguration = authenticationConfiguration;
                evaOptions.EvaCachingKeys = cachingKeys;
                evaOptions.ExternalServicesUri = evaExternalApis;
                evaOptions.EvaConventions = new EvaConventions()
                {
                    ControllerModelConvention = new EvaControllerModelConvention()
                };
            });

            // Build Eva application
            app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    o.DefaultModelExpandDepth(-1);
                });
            }

            // Authentication, Authorization and Middlewares
            app
                .UseHttpsRedirection()
                .UseAuthentication()
                .UseAuthorization()
                .UseMiddleware<EvaLoggingMiddleware>()
                .UseMiddleware<EvaExceptionMiddleware>();

            // Map Controllers
            app.MapControllers();

            // Return Web application builder
            return builder;
        }
    }
}
