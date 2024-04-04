using Eva.Core.ApplicationService.Encryptors;
using Eva.Core.ApplicationService.ExternalServices.OpenMeteo;
using Eva.Core.ApplicationService.Queries;
using Eva.Core.ApplicationService.Services;
using Eva.Core.ApplicationService.Services.Authenticators;
using Eva.Core.ApplicationService.TokenGenerators;
using Eva.Core.ApplicationService.TokenValidators;
using Eva.Core.ApplicationService.Validators;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.EndPoint.API.Authorization;
using Eva.Infra.EntityFramework.DbContexts;
using Eva.Infra.EntityFramework.Interceptors;
using Eva.Infra.Tools.Reflections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Eva.EndPoint.API.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddEvaAuthenticationConfiguration(this IServiceCollection services, AuthenticationConfiguration configuration)
        {
            services.AddSingleton(configuration);
            return services;
        }
        public static IServiceCollection AddEvaInitialInstances(this IServiceCollection services, IEnumerable<object> objects)
        {
            foreach (object obj in objects)
            {
                services.AddSingleton(obj);
            }
            return services;
        }
        public static IServiceCollection AddEvaExternalServices(this IServiceCollection services)
        {
            // Open Meteo Service for weather forcast
            services.AddHttpClient<IOpenMeteoService, OpenMeteoService>();
            return services;
        }
        public static IServiceCollection AddEvaSwagger(this IServiceCollection services)
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
        public static IServiceCollection AddEvaAuthentication(this IServiceCollection services, AuthenticationConfiguration configuration)
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
        public static IServiceCollection AddEvaControllers(this IServiceCollection services, params IControllerModelConvention[] conventions)
        {
            foreach (var convention in conventions)
            {
                services.AddControllers(s => s.Conventions.Add(convention));
            }
            return services;
        }
        public static IServiceCollection AddEvaDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IEvaDbContextFactory, EvaDbContextFactory>();
            services.AddDbContextFactory<EvaDbContext>(options => options.UseSqlite(connectionString).AddInterceptors(new SoftDeleteInterceptor()));
            return services;
        }

        public static IServiceCollection AddEvaRoleBasedAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, RoleAuthorizationPolicyProvider>();
            return services;
        }

        public static IServiceCollection AddEvaUserContext(this IServiceCollection services)
        {
            services.AddSingleton<IUserContext, UserContext>();
            return services;
        }

        public static IServiceCollection AddEvaAccessTokenGenerator(this IServiceCollection services)
        {
            services.AddSingleton<AccessTokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddScoped<Authenticator>();
            services.AddSingleton<TokenGenerator>();
            return services;
        }
        public static IServiceCollection AddEvaEntityValidators(this IServiceCollection services)
        {
            services.AddSingleton<UserValidator>();
            return services;
        }
        public static IServiceCollection AddEvaCryptographyServices(this IServiceCollection services)
        {
            services.AddSingleton<AesEncryptor>();
            services.AddSingleton<DesEncryptor>();
            services.AddSingleton<RsaEncryptor>();
            services.AddSingleton<RsaParser>();
            return services;
        }
        public static IServiceCollection AddEvaServices(this IServiceCollection services)
        {
            // Register base services
            services.AddSingleton(typeof(IBaseService<,>), typeof(BaseService<,>));

            // Get all services corresponding to Registration Required Attribute
            var repositoryTypes = Assemblies.GetEvaTypes(typeof(IBaseService<,>)).Where(t => t.IsDefined(typeof(RegistrationRequiredAttribute), true));

            // Register each service
            foreach (var repositoryType in repositoryTypes)
            {
                var repositoryInterface = repositoryType.GetInterfaces().Where(i => !i.IsGenericType).FirstOrDefault();
                services.AddSingleton(repositoryInterface, repositoryType);
            }

            // Return extension method value
            return services;
        }
    }
}
