using Eva.EndPoint.API.Conventions;
using Eva.EndPoint.API.Middlewares;

namespace Eva.EndPoint.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddEva(this WebApplicationBuilder builder, out WebApplication app)
        {
            // Accessing an IConfiguration instance to reach appsettings.json
            var configuration = builder.GetEvaConfigurations();

            // Get Connection string
            var connectionString = configuration.GetEvaConnectionString();

            // Get Eva Authentication Configuration
            var authenticationConfiguration = configuration.GetEvaAuthenticationConfiguration();

            // Add Eva required services
            builder.Services
                .AddEvaAuthenticationConfiguration(authenticationConfiguration)
                .AddEvaConfigurationEntities(configuration)
                .AddEvaControllers(new EvaControllerModelConvention())
                .AddEvaAuthentication(authenticationConfiguration)
                .AddEndpointsApiExplorer()
                .AddEvaSwagger()
                .AddEvaExternalServices()
                .AddHttpContextAccessor()
                .AddEvaUserContext()
                .AddEvaDbContext(connectionString)
                .AddEvaAccessTokenGenerator()
                .AddEvaEntityValidators()
                .AddEvaCryptographyServices()
                .AddEvaRoleBasedAuthorization()
                .AddEvaServices();

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
