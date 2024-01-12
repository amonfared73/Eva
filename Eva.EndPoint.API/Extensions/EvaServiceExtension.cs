using Eva.Core.ApplicationService.Queries;
using Eva.Core.ApplicationService.Services;
using Eva.Core.ApplicationService.Services.Authenticators;
using Eva.Core.ApplicationService.TokenGenerators;
using Eva.Core.ApplicationService.TokenValidators;
using Eva.Core.Domain.Attributes;
using Eva.Core.Domain.BaseModels;
using Eva.EndPoint.API.Conventions;
using Eva.Infra.EntityFramework.DbContextes;
using Eva.Infra.Tools.Reflections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

        private static IServiceCollection AddAccessTokenGenerator(this IServiceCollection services)
        {
            services.AddSingleton<AccessTokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddScoped<Authenticator>();
            services.AddSingleton<TokenGenerator>();
            return services;
        }

        private static IServiceCollection AddEvaServices(this IServiceCollection services)
        {
            // Register base services
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));

            // Get all services corresponding to Registration Required Attribute
            var repositoryTypes = Assemblies.GetServices("Eva.Core.ApplicationService", typeof(RegistrationRequiredAttribute));


            // Register each service
            foreach (var repositoryType in repositoryTypes)
            {
                var repositoryInterface = repositoryType.GetInterfaces().Where(i => !i.IsGenericType).FirstOrDefault();
                services.AddScoped(repositoryInterface, repositoryType);
            }

            // Return extension method value
            return services;
        }

        public static WebApplicationBuilder AddEva(this WebApplicationBuilder builder, out WebApplication app)
        {
            // Authentication configuration
            var authenticationConfiguration = new AuthenticationConfiguration();
            var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("sqlite");
            configuration.Bind("Authentication", authenticationConfiguration);
            builder.Services.AddSingleton(authenticationConfiguration);

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

            // Add DbContext
            builder.Services.AddEvaDbContext(connectionString);

            // Add Access Token Generator
            builder.Services.AddAccessTokenGenerator();

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
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            return builder;
        }
    }
}
