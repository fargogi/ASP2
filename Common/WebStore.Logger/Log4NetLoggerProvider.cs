using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;

namespace WebStore.Logger
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly string _configurationFile;
        private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new ConcurrentDictionary<string, Log4NetLogger>();

        public Log4NetLoggerProvider(string configurationFile) => _configurationFile = configurationFile;

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, category =>
            {
                var xml = new XmlDocument();
                var fileName = _configurationFile;
                if (!Path.IsPathRooted(fileName))
                {
                    var dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    fileName = Path.Combine(dir, fileName);
                }
                xml.Load(fileName);
                return new Log4NetLogger(category, xml["log4net"]);
            });
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
