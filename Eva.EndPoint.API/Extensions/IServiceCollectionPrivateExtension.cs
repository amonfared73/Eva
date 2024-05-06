using Eva.Core.ApplicationService.Encryptors;
using Eva.Core.ApplicationService.ExternalServices.OpenMeteo;
using Eva.Core.ApplicationService.Queries;
using Eva.Core.ApplicationService.Services;
using Eva.Core.ApplicationService.Services.Authenticators;
using Eva.Core.ApplicationService.TokenGenerators;
using Eva.Core.ApplicationService.TokenValidators;
using Eva.Core.ApplicationService.Validators;
using Eva.Core.Domain.Attributes.LifeTimeCycle;
using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Enums;
using Eva.EndPoint.API.Authorization;
using Eva.EndPoint.API.Conventions;
using Eva.Infra.EntityFramework.DbContexts;
using Eva.Infra.EntityFramework.Interceptors;
using Eva.Infra.Tools.Extensions;
using Eva.Infra.Tools.Reflections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace Eva.EndPoint.API.Extensions
{
    public static partial class IServiceCollectionExtension
    {
        /// <summary>
        /// Creating <see href="https://github.com/amonfared73/Eva">Eva</see> required instances as configurations for particular services
        /// This method currently instantiate <see cref="AesEncryptionConfiguration"/>, <see cref="DesEncryptionConfiguration"/>, <see cref="RsaCryptographyConfiguration"/> for cryptography services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        public static IServiceCollection AddEvaConfigurationEntities(this IServiceCollection services, IConfiguration configuration)
        {
            // AES Cryptography Configuration
            var aesEncryptionConfiguration = new AesEncryptionConfiguration();
            configuration.Bind("AesEncryptionConfiguration", aesEncryptionConfiguration);
            services.AddSingleton(aesEncryptionConfiguration);

            // DES Cryptography Configuration
            var desEncryptionConfiguration = new DesEncryptionConfiguration();
            configuration.Bind("DesEncryptionConfiguration", desEncryptionConfiguration);
            services.AddSingleton(desEncryptionConfiguration);

            // RSA Cryptography Configuration
            var rsaCryptographyConfiguration = new RsaCryptographyConfiguration();
            configuration.Bind("RsaCryptographyConfiguration", rsaCryptographyConfiguration);
            services.AddSingleton(rsaCryptographyConfiguration);

            return services;
        }
        /// <summary>
        /// Creating an <see href="https://github.com/amonfared73/Eva">Eva</see> <see cref="AuthenticationConfiguration" /> instance for generating Json Web Tokens
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        private static IServiceCollection AddEvaAuthenticationConfiguration(this IServiceCollection services, AuthenticationConfiguration configuration)
        {
            services.AddSingleton(configuration);
            return services;
        }
        /// <summary>
        /// Registering <see href="https://github.com/amonfared73/Eva">Eva</see> External services
        /// </summary>
        /// <param name="services"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        private static IServiceCollection AddEvaExternalServices(this IServiceCollection services)
        {
            // Open Meteo Service for weather forecast
            services.AddHttpClient<IOpenMeteoService, OpenMeteoService>();
            return services;
        }
        /// <summary>
        /// Add <see href="https://github.com/amonfared73/Eva">Eva</see> SwaggerGen
        /// </summary>
        /// <param name="services"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        private static IServiceCollection AddEvaSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
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
            return services;
        }
        /// <summary>
        /// Configuring <see href="https://github.com/amonfared73/Eva">Eva</see> token validation parameters for Authentication
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        private static IServiceCollection AddEvaAuthentication(this IServiceCollection services, AuthenticationConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.AccessTokenSecret)),
                    ValidIssuer = configuration.Issuer,
                    ValidAudience = configuration.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
        /// <summary>
        /// Adding <see href="https://github.com/amonfared73/Eva">Eva</see> Controllers with their respective <see cref="IApplicationModelConvention"/> conventions
        /// </summary>
        /// <param name="services"></param>
        /// <param name="conventions"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        private static IServiceCollection AddEvaControllers(this IServiceCollection services, EvaConventions evaConventions)
        {
            services.AddControllers(s =>
            {
                if (evaConventions.ApplicationModelConvention != null) s.Conventions.Add(evaConventions.ApplicationModelConvention);
                if (evaConventions.ControllerModelConvention != null) s.Conventions.Add(evaConventions.ControllerModelConvention);
                if (evaConventions.ActionModelConvention != null) s.Conventions.Add(evaConventions.ActionModelConvention);
                if (evaConventions.ParameterModelConvention != null) s.Conventions.Add(evaConventions.ParameterModelConvention);
            })
                .AddJsonOptions(j => j.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            return services;
        }
        /// <summary>
        /// Adding <see href="https://github.com/amonfared73/Eva">Eva</see> Db context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        private static IServiceCollection AddEvaDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IEvaDbContextFactory, EvaDbContextFactory>();
            services.AddDbContextFactory<EvaDbContext>(options =>
            {
                options.UseSqlite(connectionString).AddInterceptors(new SoftDeleteInterceptor());
            });
            return services;
        }
        /// <summary>
        /// Adding <see href="https://github.com/amonfared73/Eva">Eva</see> Role Based Authorization
        /// </summary>
        /// <param name="services"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        private static IServiceCollection AddEvaRoleBasedAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, RoleAuthorizationPolicyProvider>();
            return services;
        }
        /// <summary>
        /// Adding <see href="https://github.com/amonfared73/Eva">Eva</see> User Context implementation for Audit log
        /// </summary>
        /// <param name="services"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        private static IServiceCollection AddEvaUserContext(this IServiceCollection services)
        {
            services.AddSingleton<IUserContext, UserContext>();
            return services;
        }
        /// <summary>
        /// Adding services required for <see href="https://github.com/amonfared73/Eva">Eva</see> Token generation
        /// </summary>
        /// <param name="services"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        private static IServiceCollection AddEvaAccessTokenGenerator(this IServiceCollection services)
        {
            services.AddSingleton<AccessTokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddScoped<Authenticator>();
            services.AddSingleton<TokenGenerator>();
            return services;
        }
        private static IServiceCollection AddEvaEntityValidators(this IServiceCollection services)
        {
            services.AddSingleton<UserValidator>();
            services.AddSingleton<AccountValidator>();
            return services;
        }
        /// <summary>
        /// Adding services required for Encryption in <see href="https://github.com/amonfared73/Eva">Eva</see>
        /// </summary>
        /// <param name="services"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        private static IServiceCollection AddEvaCryptographyServices(this IServiceCollection services)
        {
            services.AddSingleton<AesEncryptor>();
            services.AddSingleton<DesEncryptor>();
            services.AddSingleton<RsaEncryptor>();
            services.AddSingleton<RsaParser>();
            return services;
        }
        /// <summary>
        /// Registering <see href="https://github.com/amonfared73/Eva">Eva</see> business logic application services through Reflection
        /// Classes being decorated with <see cref="RegistrationRequiredAttribute" /> will be automatically registered with respect to its <see cref="RegistrationType" />
        /// </summary>
        /// <param name="services"></param>
        /// <returns><see cref="IServiceCollection" /> of <see href="https://github.com/amonfared73/Eva">Eva</see> services</returns>
        private static IServiceCollection AddEvaServices(this IServiceCollection services)
        {
            // Register base services
            services.AddSingleton(typeof(IBaseService<,>), typeof(BaseService<,>));

            // Get all services corresponding to Registration Required Attribute
            var types = Assemblies.GetEvaTypes(typeof(IBaseService<,>)).Where(t => t.IsDefined(typeof(RegistrationRequiredAttribute), true));

            // Register each service
            foreach (var type in types)
            {
                var repositoryInterface = type.GetInterfaces().Where(i => !i.IsGenericType).FirstOrDefault();

                if (type.IsSingleton())
                    services.AddSingleton(repositoryInterface, type);
                else if (type.IsTransient())
                    services.AddTransient(repositoryInterface, type);
                else if (type.IsScoped())
                    services.AddScoped(repositoryInterface, type);

            }

            // Return extension method value
            return services;
        }
    }
}
