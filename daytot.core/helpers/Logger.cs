using System;

using log4net;

namespace daytot.core.helpers
{
    public static class Logger
    {
        private static ILog _error;
        private static ILog _info;
        public static void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            _error = log4net.LogManager.GetLogger("ERROR");
            _info = log4net.LogManager.GetLogger("INFO");
        }
        public static void Error(string zone, Exception ex)
        {
            _error.Error(zone, ex);
        }
        public static void Info(string message)
        {
            _info.Info(message);
        }

    }
}
