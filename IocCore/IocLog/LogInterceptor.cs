using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Castle.Windsor;
using IocCore.Helper;

namespace IocCore.IocLog
{
    public class LogInterceptor : IocInterceptor
    {
 
        private void PreProceed(IInvocation invocation)
        {
            LogAttribute attribute = AttributeHelper.GetAttribute<LogAttribute>(invocation) as LogAttribute;
            
            if (attribute != null && attribute.IsPreLog)
            {
                //拦截器log屏蔽
                if (LogLevel <= attribute.LogLevel)
                {
                    StringBuilder logstr = new StringBuilder();
                    logstr.AppendFormat("{0} -----Pre log", InterceptorHelper.GetMethodInfo(invocation));
                    Log.Log(logstr.ToString(), attribute.LogLevel);
                }
            }
        }
        
        private void PostProceed(IInvocation invocation)
        {
            LogAttribute attribute = AttributeHelper.GetAttribute<LogAttribute>(invocation) as LogAttribute;
            
            if (attribute != null && attribute.IsPostLog)
            {
                //拦截器log屏蔽
                if (LogLevel <= attribute.LogLevel)
                {
                    StringBuilder logstr = new StringBuilder();
                    logstr.AppendFormat("{0} -----Post log", InterceptorHelper.GetMethodInfo(invocation));
                    Log.Log(logstr.ToString(), attribute.LogLevel);
                }
            }
        }
        public override void Intercept(IInvocation invocation)
        {
            this.PreProceed(invocation);
            invocation.Proceed();
            this.PostProceed(invocation);
        }

        
        
    }
}
