using HotelLocker.DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql;
using System;
using HotelLocker.DAL.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using HotelLocker.BLL.Configurations;
using HotelLocker.DAL.Interfaces;
using HotelLocker.DAL.UoW;
using HotelLocker.BLL.Interfaces;
using HotelLocker.BLL.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelLocker.BLL.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static AuthenticationBuilder AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddServices(configuration)
                .AddIdentity(configuration);
            return services.AddTokenAuthentication(configuration);
        }

        public static IdentityBuilder AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddIdentity<User, IdentityRole<int>>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireDigit = true;
                }
                )
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<HotelContext>()
                .AddDefaultTokenProviders();
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<HotelContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton)
                .AddSingleton<IUnitOfWork, UnitOfWork>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IHotelService, HotelService>()
                .AddScoped<IRoomService, RoomService>()
                .AddScoped<IReservationService, ReservationService>();
        }

        private static AuthenticationBuilder AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            AuthConfiguration authConfiguration = new AuthConfiguration(configuration);
            return services
                .AddSingleton(authConfiguration)
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions => {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = authConfiguration.Key,

                        ValidateIssuer = true,
                        ValidIssuer = authConfiguration.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authConfiguration.Audience,

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
