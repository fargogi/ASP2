using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(string name, XmlElement xml)
        {
            var logger_repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            _log = LogManager.GetLogger(logger_repository.Name, name);

            log4net.Config.XmlConfigurator.Configure(logger_repository, xml);
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    return _log.IsDebugEnabled;
                case LogLevel.Information:
                    return _log.IsInfoEnabled;
                case LogLevel.Warning:
                    return _log.IsWarnEnabled;
                case LogLevel.Error:
                    return _log.IsErrorEnabled;
                case LogLevel.Critical:
                    return _log.IsFatalEnabled;
                case LogLevel.None:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        public void Log<TState>(LogLevel level, EventId id, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(level)) return;
            if (formatter is null) throw new ArgumentNullException(nameof(formatter));

            var msg = formatter(state, exception);

            if (string.IsNullOrEmpty(msg) && exception is null) return;

            switch (level)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    _log.Debug(msg);
                    break;
                case LogLevel.Information:
                    _log.Info(msg);
                    break;
                case LogLevel.Warning:
                    _log.Warn(msg);
                    break;
                case LogLevel.Error:
                    _log.Error(msg);
                    break;
                case LogLevel.Critical:
                    _log.Fatal(msg);
                    break;
                case LogLevel.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }
}
