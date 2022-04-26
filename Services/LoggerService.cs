using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEducation.Services
{
    public class LoggerService : ILoggerService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void logDebug(string message)
        {
            logger.Debug(message);
        }

        public void logInfo(string message)
        {
            logger.Info(message);
        }

        public void logWarm(string message)
        {
            logger.Warn(message);
        }
    }
}
