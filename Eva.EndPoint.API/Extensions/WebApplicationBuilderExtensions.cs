using Eva.Core.ApplicationService.ExternalServices.OpenMeteo;
using Eva.Core.Domain.BaseModels;
using Eva.EndPoint.API.Conventions;
using Eva.EndPoint.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Eva.EndPoint.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Initializes <see href="https://github.com/amonfared73/Eva">Eva</see> Framework asp.Net Core web api application
        /// <para>
        /// Grabing sensitive configuration data from <see cref="IConfiguration" />
        /// <para>Creating instances of related objects and injecting them into HTTP pipeline</para>
        /// <para>Configuring controllers with their custom respective <see cref="IControllerModelConvention" /></para>
        /// <para>Configuring authentication and authorization</para>
        /// <para>Registering <see href="https://github.com/amonfared73/Eva">Eva</see> services</para>
        /// <para>Adding related middlewares to HTTP pipeline</para>
        /// </para>
        /// <para>
        /// <param name="app">an <see href="https://github.com/amonfared73/Eva">Eva</see> <see cref="WebApplication" /> out parameter used to configure the HTTP pipeline and routes</param>
        /// </para>
        /// </summary>
        /// <returns>
        /// A <see cref="WebApplicationBuilder" /> that represents the <see href="https://github.com/amonfared73/Eva">Eva</see> Framework builder
        /// </returns>
        public static WebApplicationBuilder AddEva(this WebApplicationBuilder builder, out WebApplication app)
        {
            // Connection string
            var configuration = builder.GetEvaConfigurations();
            var connectionString = configuration.GetEvaConnectionString();

            // Authentication configuration
            var authenticationConfiguration = configuration.GetEvaAuthenticationConfiguration();

            // AES Cryptography Configuration
            var aesEncryptionConfiguration = new AesEncryptionConfiguration();
            configuration.Bind("AesEncryptionConfiguration", aesEncryptionConfiguration);

            // DES Cryptography Configuration
            var desEncryptionConfiguration = new DesEncryptionConfiguration();
            configuration.Bind("DesEncryptionConfiguration", desEncryptionConfiguration);

            // RSA Cryptography Configuration
            var rsaCryptographyConfiguration = new RsaCryptographyConfiguration();
            configuration.Bind("RsaCryptographyConfiguration", rsaCryptographyConfiguration);

            builder.Services
                .AddEvaAuthenticationConfiguration(authenticationConfiguration)
                .AddSingleton(aesEncryptionConfiguration)
                .AddSingleton(desEncryptionConfiguration)
                .AddSingleton(rsaCryptographyConfiguration)
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

        private static IConfiguration GetEvaConfigurations(this WebApplicationBuilder builder)
        {
            return builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        }
    }
}
