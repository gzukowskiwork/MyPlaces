using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyPlaces.Installers.Services
{
    public class AddIentityService : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
        }
    }
}
