using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IocCore.IocLog;

namespace IocCore.IocExHandle
{
    [Flags]
    public enum ExHandleOption { ignore, throwex, log ,all};

    [AttributeUsage(AttributeTargets.All)]
    public class ExHandleAttribute : Attribute
    {
        public virtual ExHandleOption Option { get; set; }
        public virtual LogLevel LogLevel { get; set; }
        public virtual bool IsIgnore
        {
            get { return Option == ExHandleOption.ignore; }
        }
        public virtual bool IsThrow
        {
            get { return (Option & ExHandleOption.throwex) != 0;}
        }
        public virtual bool IsLog
        {
            get { return (Option & ExHandleOption.log) != 0; }
        }
    }
}
