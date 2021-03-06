using AutoMapper;
using EmailService;
using Entities.Model;
using Entities.Model.Repository;
using Identity.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyPlaces.Installers.ServicesExtensions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

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
            services.ConfigureCors();
            services.DbConnection(Configuration);

            services.AddIdentity();
            services.AddAuthenticationService(Configuration);

            services.AddRepository();


            services.AddAutoMapper();
            services.AddIdentityService();

            services.AddControllers();

            services.AddSwaggerGen();

            services.EmailConfiguration(Configuration);

            #region dupa
            // services.AddCors(options =>
            // {
            //     options.AddPolicy("CorsPolicy",
            //         builder => builder.AllowAnyOrigin()
            //         .AllowAnyMethod()
            //         .AllowAnyHeader());
            // });

            // string connectionString = Configuration["connection:connectionString"];
            // services.AddDbContext<ApplicationContext>(o =>
            //     o.UseSqlServer(connectionString));

            // services.AddAuthentication(auth =>
            // {
            //     auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // }).AddJwtBearer(o =>
            // {
            //     o.SaveToken = true;
            //     o.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuer = true,
            //         ValidateAudience = true,
            //         ValidAudience = Configuration["Authenticate:Audience"],
            //         ValidIssuer = Configuration["Authenticate:Issuer"],

            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authenticate:Key"])),
            //         ValidateIssuerSigningKey = true
            //     };
            // });
            // services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("RegisteredUser",
            //         builder => builder.RequireClaim("RegisteredUser", "true"));
            // });

            // services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            // {
            //     o.Password.RequiredLength = 8;
            // }).AddEntityFrameworkStores<ApplicationContext>()
            //.AddDefaultTokenProviders();

            // services.AddScoped<IPlaceRepository, PlaceRepository>();

            // services.AddAutoMapper(typeof(Startup));

            // services.AddScoped<IIdentityService, IdentityService>();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyPlaces", Version = "v1" });
            //    var security = new Dictionary<string, IEnumerable<string>>
            //    {
            //        {"Bearer", Array.Empty<string>() }
            //    };
            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] " +
            //                        "and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer"
            //    });

            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference
            //            {
            //                Type = ReferenceType.SecurityScheme,
            //                Id = "Bearer"
            //            },
            //            Name = "Bearer",
            //            In = ParameterLocation.Header,
            //        },
            //        new List<string>()
            //    }});
            //});

            //var emailConfiguration = Configuration.GetSection("EmailConfiguration").Get<EmailConfig>();
            //services.AddSingleton(emailConfiguration);
            //services.AddScoped<IEmailEmitter, EmailEmitter>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyPlaces v1");
                    c.EnableValidator();
                });
            }
            app.UseCors("CorsPolicy");
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
