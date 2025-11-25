using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billings;
using BarberBoss.Infraestructure.DataAccess;
using BarberBoss.Infraestructure.DataAccess.Repositories.Billings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddDbContext(services, configuration);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBillingsWriteOnlyRepository, BilllingsRepository>();
            services.AddScoped<IBillingsReadOnlyRepository, BilllingsRepository>();
            services.AddScoped<IBillingsUpdateOnlyRepository, BilllingsRepository>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");

            var version = new Version(8, 0, 44);
            var serverVersion = new MySqlServerVersion(version);

            services.AddDbContext<BarberBossDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }
    }
}
