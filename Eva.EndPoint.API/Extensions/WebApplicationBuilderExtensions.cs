using Eva.EndPoint.API.Conventions;
using Eva.EndPoint.API.Middlewares;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Eva.EndPoint.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddEva(this WebApplicationBuilder builder, out WebApplication app)
        {
            var configuration = builder.GetEvaConfigurations();

            var connectionString = configuration.GetEvaConnectionString();

            var authenticationConfiguration = configuration.GetEvaAuthenticationConfiguration();

            var configs = configuration.GetEvaEntityConfigurations();

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

            app
                .UseHttpsRedirection()
                .UseAuthentication()
                .UseAuthorization()
                .UseMiddleware<EvaLoggingMiddleware>()
                .UseMiddleware<EvaExceptionMiddleware>();


            app.MapControllers();

            return builder;
        }
    }
}
