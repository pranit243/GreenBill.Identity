using GreenBill.Identity.Application.Authentication.Interfaces;
using GreenBill.Identity.Infrastructure.Authentication;
using GreenBill.Identity.Infrastructure.Persistent;
using GreenBill.Identity.Infrastructure.Persistent.Repositories;
using GreenBill.Identity.Infrastructure.Persistent.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreenBill.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("IdentityDatabase")));

            services.Configure<JwtSettings>(
                configuration.GetSection("Jwt"));

            services.AddScoped<IdentityDbSeeder>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            return services;
        }
    }
}
