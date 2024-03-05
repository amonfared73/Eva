using Eva.Core.ApplicationService.Encryptors;
using Eva.Core.ApplicationService.ExternalServices.OpenMeteo;
using Eva.Core.ApplicationService.Queries;
using Eva.Core.ApplicationService.Services;
using Eva.Core.ApplicationService.Services.Authenticators;
using Eva.Core.ApplicationService.TokenGenerators;
using Eva.Core.ApplicationService.TokenValidators;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models.Cryptography;
using Eva.EndPoint.API.Authorization;
using Eva.EndPoint.API.Conventions;
using Eva.EndPoint.API.Middlewares;
using Eva.Infra.EntityFramework.DbContextes;
using Eva.Infra.Tools.Reflections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace Eva.EndPoint.API.Extensions
{
    public static class EvaServiceExtension
    {
        private static IServiceCollection AddEvaDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EvaDbContext>(options => options.UseSqlite(connectionString), optionsLifetime: ServiceLifetime.Singleton);
            services.AddDbContextFactory<EvaDbContext, EvaDbContextFactory>(options => options.UseSqlite(connectionString));
            return services;
        }

        private static IServiceCollection AddEvaRoleBasedAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, RoleAuthorizationPolicyProvider>();
            return services;
        }

        private static IServiceCollection AddAccessTokenGenerator(this IServiceCollection services)
        {
            services.AddSingleton<AccessTokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddScoped<Authenticator>();
            services.AddSingleton<TokenGenerator>();
            return services;
        }
        private static IServiceCollection AddCryptographyServices(this IServiceCollection services)
        {
            services.AddSingleton<AesEncryptor>();
            services.AddSingleton<DesEncryptor>();
            services.AddSingleton<RsaKeyPair>();
            services.AddSingleton<RsaEncryptor>();
            return services;
        }
        private static IServiceCollection AddEvaServices(this IServiceCollection services)
        {
            // Register base services
            services.AddSingleton(typeof(IBaseService<>), typeof(BaseService<>));

            // Get all services corresponding to Registration Required Attribute
            var repositoryTypes = Assemblies.GetServices("Eva.Core.ApplicationService", typeof(RegistrationRequiredAttribute));


            // Register each service
            foreach (var repositoryType in repositoryTypes)
            {
                var repositoryInterface = repositoryType.GetInterfaces().Where(i => !i.IsGenericType).FirstOrDefault();
                services.AddSingleton(repositoryInterface, repositoryType);
            }

            // Return extension method value
            return services;
        }

        /// <summary>
        /// Initializes Eva framework for asp.Net Core Web API application
        /// </summary>
        /// <param name="builder">WebApplicationBuilder extension method</param>
        /// <param name="app">WebApplication out parameter</param>
        /// <returns></returns>
        public static WebApplicationBuilder AddEva(this WebApplicationBuilder builder, out WebApplication app)
        {
            // Connection string
            var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("sqlite");

            // Authentication configuration
            var authenticationConfiguration = new AuthenticationConfiguration();
            configuration.Bind("Authentication", authenticationConfiguration);
            builder.Services.AddSingleton(authenticationConfiguration);

            // AES Cryptography Configuration
            var aesEncryptionConfiguration = new AesEncryptionConfiguration();
            configuration.Bind("AesEncryptionConfiguration", aesEncryptionConfiguration);
            builder.Services.AddSingleton(aesEncryptionConfiguration);

            // DES Cryptography Configuration
            var desEncryptionConfiguration = new DesEncryptionConfiguration();
            configuration.Bind("DesEncryptionConfiguration", desEncryptionConfiguration);
            builder.Services.AddSingleton(desEncryptionConfiguration);

            // RSA Cryptography Configuration
            var rsaCryptographyConfiguration = new RsaCryptographyConfiguration();
            configuration.Bind("RsaCryptographyConfiguration", rsaCryptographyConfiguration);
            builder.Services.AddSingleton(rsaCryptographyConfiguration);

            builder.Services.AddControllers(s => s.Conventions.Add(new EvaControllerModelConvention()));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
                    ValidIssuer = authenticationConfiguration.Issuer,
                    ValidAudience = authenticationConfiguration.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(
                    name: JwtBearerDefaults.AuthenticationScheme,
                    securityScheme: new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Description = "Enter the Bearer Authorization : `Bearer Generated-JWT-Token`",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    }
                    );
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                    }
                        ,new string[] {}
                    }
                });
            });

            // Add HtppClients
            // Open Meteo Service for weather forcast
            builder.Services.AddHttpClient<IOpenMeteoService, OpenMeteoService>();

            // Add DbContext
            builder.Services.AddEvaDbContext(connectionString);

            // Add Access Token Generator
            builder.Services.AddAccessTokenGenerator();

            // Add Cryptography 
            builder.Services.AddCryptographyServices();

            // Add Role based authorization
            builder.Services.AddEvaRoleBasedAuthorization();

            // Add Services
            builder.Services.AddEvaServices();

            app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    o.DefaultModelExpandDepth(-1);
                });
            }


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<EvaLoggingMiddleware>();
            app.UseMiddleware<EvaExceptionMiddleware>();

            app.MapControllers();

            return builder;
        }
    }
}
