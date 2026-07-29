using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Repositories;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API
{
    public static class DependencyInjectionContainer
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}