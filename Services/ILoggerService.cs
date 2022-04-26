using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEducation.Services
{
    public interface ILoggerService
    {
        void logInfo(String message);
        void logWarm(String message);
        void logDebug(String message);
    }
}
