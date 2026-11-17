using ManagmentSystem.Core.Models;
using ManagmentSystem.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace ManagmentSystem.Pres
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Add this line to write logs to a file
            .CreateLogger();

            try
            {
                Log.Information("Starting up the application");
                CreateHostBuilder(args).Build().Run();  //without seed
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to start correctly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            ////  with seed  //
            //var host = CreateHostBuilder(args).Build();
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var context = services.GetRequiredService<ApplicationDBContext>();
            //    SeedData(context);
            //}
            //host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()  // Add this line to use Serilog as the logging provider
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void SeedData(ApplicationDBContext context)
        {
            var listOfCategories = new List<Category>
            {
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A6",
                    AName = "A6",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A7",
                    AName = "A7",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A8",
                    AName = "A8",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A9",
                    AName = "A9",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A10",
                    AName = "A10",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A11",
                    AName = "A11",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A12",
                    AName = "A12",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A13",
                    AName = "A13",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A14",
                    AName = "A14",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A15",
                    AName = "A15",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A16",
                    AName = "A16",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A17",
                    AName = "A17",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A18",
                    AName = "A18",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A19",
                    AName = "A19",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A20",
                    AName = "A20",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A21",
                    AName = "A21",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A22",
                    AName = "A22",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A23",
                    AName = "A23",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A24",
                    AName = "A24",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A25",
                    AName = "A25",
                }
            };
            foreach (var item in listOfCategories)
            {
                context.Categories.Add(item);
            }
                context.SaveChanges();
        }
    }
}
