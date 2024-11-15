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

namespace ManagmentSystem.Pres
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();      //without seed

            //  with seed  //
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
                    EName = "A1",
                    AName = "A1",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A2",
                    AName = "A2",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A3",
                    AName = "A3",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A4",
                    AName = "A4",
                },
                new() {
                    Id = Guid.NewGuid().ToString(),
                    EName = "A5",
                    AName = "A5",
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
