using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Repositories;
using TaskManagement.API.Repositories.Interfaces;
using TaskManagement.API.Services;
using TaskManagement.API.Services.Interfaces;

namespace TaskManagement.API
{
    /// <summary>
    /// Provides extension methods for registering application dependencies.
    /// </summary>
    public static class DependencyInjectionContainer
    {
        /// <summary>
        /// Registers database context, repositories, and application services into the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection used for dependency registration.</param>
        /// <param name="configuration">Application configuration containing database settings.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
            });

            // Repositories
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IUserService, UserService>();

            // Other Services
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}