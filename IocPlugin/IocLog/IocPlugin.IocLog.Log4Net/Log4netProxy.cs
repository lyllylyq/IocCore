using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IocCore.IocLog;

namespace IocPlugin.IocLog.Log4Net
{
    public class Log4netProxy : ILog
    {
        public string ConfigFile { get; set; }
        private static log4net.ILog log = null;
        private log4net.ILog GetLogInstence()
        {
            if (log == null)
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo(ConfigFile));
                log = log4net.LogManager.GetLogger("DebugLogging");
            }
            return log;
        }
        public void Log(string msg)
        {
            log4net.ILog log =GetLogInstence();
            log.Debug(msg);
        }
        public void Log(string msg,LogLevel level)
        {
            Log(msg, level, null);
        }
        public void Log(string msg, LogLevel level,Exception ex)
        {
            log4net.ILog log = GetLogInstence();
            if ((level & LogLevel.debug) != 0||level==LogLevel.all) log.Debug(msg, ex);
            if ((level & LogLevel.info) != 0) log.Info(msg, ex);
            if ((level & LogLevel.warn) != 0) log.Warn(msg, ex);
            if ((level & LogLevel.error) != 0) log.Error(msg, ex);
            if ((level & LogLevel.fatal) != 0) log.Fatal(msg, ex);
        }

        
    }
}
