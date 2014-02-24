using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocLog
{

    public enum LogLevel { all = 0x0, debug = 0x1, info = 0x10, warn = 0x100, error=0x1000, fatal=0x10000,off=0x100000 };
    [Flags]
    public enum LogOption { none, pre, post, all };

    [AttributeUsage(AttributeTargets.All)]
    public class LogAttribute : Attribute
    {
        public virtual LogLevel LogLevel { get; set; }
        public virtual LogOption Option { get; set; }

        public virtual bool IsPreLog { get { return (Option & LogOption.pre) != 0; } }
        public virtual bool IsPostLog { get { return (Option & LogOption.post) != 0; } }

        
    }
}
