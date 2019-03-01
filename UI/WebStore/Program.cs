using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWebStore.DALNew.Context;
using WebStore.Data;

namespace MyWebStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<MyWebStoreContext>();
                    db.Initialize();
                    services.InitializeIdentityAsync().Wait();
                }
                catch (Exception e)
                {

                    services.GetRequiredService<ILogger<Program>>().
                             LogError(e, "Ошибка инициализации контекста базы данных");
                }
            }
                host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
