using Christmas.Secret.Gifter.API.HostedServices;
using Christmas.Secret.Gifter.API.HostedServices.Hub;
using Christmas.Secret.Gifter.API.Services;
using Christmas.Secret.Gifter.API.Services.Abstractions;
using Christmas.Secret.Gifter.Database.SQLite;
using Christmas.Secret.Gifter.Database.SQLite.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Christmas.Secret.Gifter.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<ILocalesStatusHub, LocalesStatusHub>();

            services.AddScoped<IImageExtractor, ImageExtractor>();
            services.AddScoped<ILocalesGenerator, LocalesGenerator>();
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IParticipantService, ParticipantService>();

            services.AddSQLLiteDatabase();
            services.AddCors();

            services.AddDbContext<GifterDbContext>(options =>
                options
                    .UseSqlite(Configuration.GetConnectionString("GifterDbContext"),
                b => b.MigrationsAssembly(typeof(GifterDbContext).Assembly.FullName)));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<GifterDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Tokens:Key"]));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    //TODO: false to true invetigate twhat needs to be corrected
                    IssuerSigningKey = signingKey,
                    ValidateAudience = false,
                    ValidAudience = Configuration["Tokens:Audience"],
                    ValidateIssuer = false,
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false
                };
            });

            services.AddSignalR();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Christmas-Secret-Gifter-API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }}, new List<string>()}
                });
            });

            services.AddHostedService<LocalesHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GifterDbContext dbContext)
        {
            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception)
            {
                Console.WriteLine("On Migrate error");
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GifterDbContext v1"));
            }

            app.UseRouting();
            app.UseHsts();

            app.UseCors(p =>
            {
                p.AllowAnyOrigin();
                p.AllowAnyHeader();
                p.AllowAnyMethod();
                //p.WithOrigins("http://localhost:3008").AllowCredentials();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<LocalesStatusHub>("/localesHub");
            });
        }
    }
}
