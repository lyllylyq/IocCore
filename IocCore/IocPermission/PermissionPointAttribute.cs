using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IocCore.IocLog;

namespace IocCore.IocPermission
{
    [Flags]
    public enum DenyHandle { giveup, alert, throwex, alertAndThrow };
    [Flags]
    public enum LogOpt { off, deny, accept, all };
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method)]
    public class PermissionPointAttribute : Attribute
    {
        public string ResolveType { get; set; }
        public string Name { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }

        public DenyHandle DenyHandle { get; set; }
        public LogOpt LogOpt { get; set; }
        public virtual LogLevel LogLevel { get; set; }
        public string DenyMsg { get; set; }

        public virtual bool IsGiveup
        {
            get { return DenyHandle == DenyHandle.giveup; }
        }
        public virtual bool IsAlert
        {
            get { return (DenyHandle & DenyHandle.alert) != 0; }
        }
        public virtual bool IsThrow
        {
            get { return (DenyHandle & DenyHandle.throwex) != 0; }
        }
        public virtual bool IsAcceptLog
        {
            get { return (LogOpt & LogOpt.accept) != 0; }
        }
        public virtual bool IsDenyLog
        {
            get { return (LogOpt & LogOpt.deny) != 0; }
        }
    }
}
