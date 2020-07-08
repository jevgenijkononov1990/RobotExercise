using System;
using System.IO;
using System.Reflection;
using log4net.Config;
using log4net.Repository;

namespace Robot.Common.Logging
{
    public static class LogManager
    {
        public static ILogger GetLogger(Type type)
        {
            return new Logger(type);
        }

        public static ILoggerRepository GetRepository(Assembly assembly)
        {
            return log4net.LogManager.GetRepository(assembly);
        }

        public static void Configure(string configFilePath)
        {
            ILoggerRepository logRepository = GetRepository(Assembly.GetEntryAssembly());
#if DEBUG
            string devPath = configFilePath?.Replace(".config", ".development.config") ?? "log4net.development.config";
            if (File.Exists(devPath))
            {
                configFilePath = devPath;
            }
#endif
            if (configFilePath != null)
            {
                XmlConfigurator.Configure(logRepository, new FileInfo(configFilePath));
            }
        }

        public static void Configure(Assembly assembly, string configFilePath)
        {
            ILoggerRepository logRepository = GetRepository(assembly);
#if DEBUG
            string devPath = configFilePath?.Replace(".config", ".development.config") ?? "log4net.development.config";
            if (File.Exists(devPath))
            {
                configFilePath = devPath;
            }
#endif
            if (configFilePath != null)
            {
                XmlConfigurator.Configure(logRepository, new FileInfo(configFilePath));
            }
        }

        public static void Configure(Assembly assembly)
        {
            ILoggerRepository logRepository = GetRepository(assembly);
            XmlConfigurator.Configure(logRepository);
        }
    }
}
