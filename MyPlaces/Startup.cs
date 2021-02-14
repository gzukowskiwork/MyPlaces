using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Entities.Model;
using Entities.Model.Repository;
using AutoMapper;
using Serilog;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Identity.Service;

namespace MyPlaces
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration["connection:connectionString"];
            services.AddDbContext<ApplicationContext>(o =>
                o.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "myValidAudience",
                    ValidIssuer = "myValidAudience",
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My symetric secret")),
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddScoped<IPlaceRepository, PlaceRepository>();
            // TODO move Models to Entities project
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
          
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyPlaces", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyPlaces v1");
                    c.EnableValidator();
                });
            }
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
            );
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
