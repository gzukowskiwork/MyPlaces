using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyPlaces.Installers.Services
{
    public class DbService : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["connection:connectionString"];
            services.AddDbContext<ApplicationContext>(o =>
                o.UseSqlServer(connectionString));
        }
    }
}
