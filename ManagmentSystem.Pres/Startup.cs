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

namespace ManagmentSystem.Pres
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
            services.AddCors();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set your timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });
            services.AddControllersWithViews();

            //services.AddDbContext<ApplicationDBContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

            //services.AddDbContext<ApplicationDBContext>(options =>
            //        options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
            //        ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));
            
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

            services.AddScoped<IAuthService, AuthService>();
            //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();


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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseSession(); // Enable session before UseEndpoints

            //للسماح بالاتصال بالتطبيق من شبكات او بورتات اخرى اي ليس محلي (لوكال).ب
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
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

                //if (env.IsDevelopment())
                //{
                //    //spa.Options.StartupTimeout = TimeSpan.FromSeconds(520);
                //    spa.UseReactDevelopmentServer(npmScript: "start");
                //}

                //if (env.IsDevelopment())
                //{
                //    // Use Vite's development server
                //    app.UseDeveloperExceptionPage();
                //    app.UseSpa(spa =>
                //    {
                //        spa.Options.SourcePath = "client"; // Path where your Vite app is located
                //        spa.UseViteDevServer(); // Use Vite development server
                //    });
                //}
                //else
                //{
                //    app.UseExceptionHandler("/Home/Error");
                //    app.UseHsts();
                //}

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
