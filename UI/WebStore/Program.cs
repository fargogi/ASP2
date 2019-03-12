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
using MyWebStore.DAL;
using WebStore.Data;
using MyWebStore.DomainNew;
using System.Xml;
using log4net;
using System.Reflection;

namespace MyWebStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var log4net_congig_xml = new XmlDocument();

            var config_file_name = "log4net.config";

            log4net_congig_xml.Load(config_file_name);

            var repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy)
                );

            log4net.Config.XmlConfigurator.Configure(repository, log4net_congig_xml["log4net"]);

            ILog log = log4net.LogManager.GetLogger(typeof(Program));

            log.Info("Запуск приложения!!!");

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
