using ManagmentSystem.Core.UnitOfWorks;
using ManagmentSystem.EF.UnitOfWorks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagmentSystem.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ManagmentSystem.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using ManagmentSystem.Core.IServices;
using ManagmentSystem.EF.Services;
using Serilog;
using System.IO;
using ManagmentSystem.Pres.Middleware;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using ManagmentSystem.Core.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.CodeAnalysis;
using StackExchange.Redis;
using Microsoft.Extensions.Caching.Distributed;

namespace ManagmentSystem.Pres
{
    public class Startup
    {
        private readonly ConnectionMultiplexer _redis;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _redis = ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnection"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => { 
                options.AddPolicy("AllowAllOrigins", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }); 
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set your timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });
            //services.AddControllersWithViews();
            
            services.AddDbContext<ApplicationDBContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 31))));

            services.Configure<JWT>(Configuration.GetSection("JWT"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                    };
                });

            // ✅ Add localization support
            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            //services.AddSingleton<IStringLocalizer<SharedResource>, StringLocalizer<SharedResource>>();

            services.AddLocalization();
            services.AddSingleton<IConnectionMultiplexer>(_redis);
            
            services.AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(ManagmentSystem.Core.Resources.SharedResource));
                });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en", "ar" };
                options.SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IPrivilegeService, PrivilegeService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // ✅ Register Global Exception Middleware
            services.AddTransient<GlobalExceptionHandler>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //var supportedCultures = new[]
            //{
            //    new CultureInfo("en"),
            //    new CultureInfo("ar")
            //};

            //var localizationOptions = new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("en"),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures
            //};

            app.UseSerilogRequestLogging();  // Add this line to enable Serilog request logging

            // Set WebRootPath manually if necessary
            env.WebRootPath ??= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            // Ensure wwwroot directory exists
            if (!Directory.Exists(env.WebRootPath))
            {
                Directory.CreateDirectory(env.WebRootPath);
            }

            // ✅ Apply the Middleware
            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // ✅ Serve static files
            app.UseSpaStaticFiles();
            //للسماح بالاتصال بالتطبيق من شبكات او بورتات اخرى اي ليس محلي (لوكال).ب
            //app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseCors("AllowAllOrigins");
            //app.UseRequestLocalization(localizationOptions);
            app.UseRequestLocalization();
            app.UseRouting();

            app.UseSession(); // Enable session before UseEndpoints

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                
                if (env.IsDevelopment())
                {
                    // Use Vite Development Server in development mode
                    // Ensure you have Vite running on its default port (e.g., 5173)
                    app.UseDeveloperExceptionPage();
                    app.UseViteDevelopmentServer(); // You may need to create this middleware.
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();
            });
        }
    }
}
