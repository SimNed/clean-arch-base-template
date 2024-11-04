using Application.Abstractions.AuthUser;
using Application.UseCases.Auth;
using Application.UseCases.DomainUsers;
using Application.UseCases.Plants;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("DefaultDb"));

            services.AddTransient<IEmailSender, EmailSenderService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IAuthUserService, AuthUserService>();

            services.AddScoped<IPlantRepository, PlantRepository>();
            services.AddScoped<IDomainUserRepository, DomainUserRepository>();

            return services;
        }
    }
}
