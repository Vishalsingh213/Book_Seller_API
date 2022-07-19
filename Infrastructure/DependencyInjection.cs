using Application.Common.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Methods;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Innoid.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("AppCon"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));




            services.AddScoped<IApplicationDbContext>(provider => (IApplicationDbContext)provider.GetService<ApplicationDbContext>());
            services.AddTransient<IMailService, SendGridMailService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IJwtDecodeWrapper, JwtDecodeWrapper>();

            //services.AddSingleton<IJwtDecodeService, JwtDecodeService>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            // Configure Authentication
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["JWT:validAudience"],
                    ValidIssuer = configuration["JWT:validIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:securityKey"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
