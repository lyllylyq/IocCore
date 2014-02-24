using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IocCore.IocLog;
using IocCore.Helper;
using Castle.DynamicProxy;

namespace IocCore
{
    public abstract class IocInterceptor : IInterceptor
    {

        private LogLevel logLevel = LogLevel.all;
        //拦截器日志级别
        public LogLevel LogLevel
        {
            get
            {
                return logLevel;
            }
            set
            {
                LogLevel gLevel = ((IocGlobalConfig)IocCoreFactory.Get<IocGlobalConfig>()).LogLevel;
                if (value < gLevel)
                    logLevel = gLevel;
                else
                    logLevel = value;
            }
        }

        private ILog log = null;
        public ILog Log
        {
            get
            {
                if (log == null)
                    log = IocCoreFactory.Get<ILog>();
                return log;
            }
        }

        public abstract void Intercept(IInvocation invocation);
    }
}
