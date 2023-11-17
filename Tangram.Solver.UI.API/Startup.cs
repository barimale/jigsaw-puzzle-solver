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
using Tangram.Solver.UI.Database.SQLite;
using Tangram.Solver.UI.Database.SQLite.Extensions;
using Tangram.Solver.UI.HostedServices;
using Tangram.Solver.UI.HostedServices.Hub;
using Tangram.Solver.UI.Services;
using Tangram.Solver.UI.Services.Abstractions;

namespace Tangram.Solver.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddScoped<IEventService, EventService>();
            services.AddSingleton<IAlgorithmDetailsService, AlgorithmDetailsService>();
            services.AddSingleton<IGamePartsDetailsService, GamePartsDetailsService>();

            services.AddSQLLiteDatabase();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("*",
                            "http://localhost:3010")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

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
                c.AddSignalRSwaggerGen();
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

            services.AddHostedService<AlgorithmWorkerHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GifterDbContext dbContext)
        {
            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("On Migrate error: " + ex.Message);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GifterDbContext v1"));
            }

            app.UseRouting();
            app.UseHsts();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<LocalesStatusHub>("/progressHub");
            });
        }
    }
}
