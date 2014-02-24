using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocLog
{
    public interface ILog
    {
        void Log(string msg);
        void Log(string msg,LogLevel level);
        void Log(string msg, LogLevel level, Exception ex);
    }
}
