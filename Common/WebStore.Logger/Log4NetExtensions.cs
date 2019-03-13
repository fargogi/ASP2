using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace WebStore.Logger
{
    public static class Log4NetExtensions
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string configurationFile = "log4net.config")
        {
            var file = new FileInfo(configurationFile);
            factory.AddProvider(new Log4NetLoggerProvider(configurationFile));

            if (!Path.IsPathRooted(configurationFile))
            {
                var assembly = Assembly.GetEntryAssembly();

                var dir = Path.GetDirectoryName(assembly.Location);

                configurationFile = Path.Combine(dir, configurationFile);
            }
            return factory;
        }
    }
}
