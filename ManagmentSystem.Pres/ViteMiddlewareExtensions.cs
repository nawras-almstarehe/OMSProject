using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManagmentSystem.Pres
{
    public static class ViteMiddlewareExtensions
    {
        public static void UseViteDevelopmentServer(this IApplicationBuilder app)
        {
            var vitePath = Path.Combine(Directory.GetCurrentDirectory(), "path-to-vite-server");
            var viteUrl = "http://localhost:5173"; // Change if your Vite server runs on a different port

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/your-vite-assets"))
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync(viteUrl + context.Request.Path);
                        context.Response.StatusCode = (int)response.StatusCode;
                        context.Response.ContentType = response.Content.Headers.ContentType.ToString();
                        await response.Content.CopyToAsync(context.Response.Body);
                        return;
                    }
                }
                await next.Invoke();
            });
        }
    }

}
