using GreenBill.Identity.Infrastructure.Authentication;
using GreenBill.Identity.Infrastructure.Persistent;
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

            return services;
        }
    }
}
