using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using IocCore.IocLog;
using IocCore.Helper;

namespace IocCore.IocExHandle
{
    public class ExHandleInterceptor : IocInterceptor
    {
        private void ExcuteExHandle(IInvocation invocation,Exception ex)
        {
            ExHandleAttribute attribute=AttributeHelper.GetAttribute<ExHandleAttribute>(invocation) as ExHandleAttribute;
            if (attribute == null)
            {
                if (ex != null)
                    throw ex;
                else
                    return;
            }
            if (attribute.IsLog)
            {
                if (LogLevel <= attribute.LogLevel)
                {
                    StringBuilder logstr = new StringBuilder();
                    logstr.AppendFormat("{0} {1} -----EXHandle ", "Exception", InterceptorHelper.GetMethodInfo(invocation));
                    Log.Log(logstr.ToString(), attribute.LogLevel, ex);
                }
            }
            if (attribute.IsThrow) throw ex;
            //若忽略则什么都不做
            if (attribute.IsIgnore) return;
        }
        public override void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                this.ExcuteExHandle(invocation, ex);
            }
        }
        private static string getExceptionInfo(Exception ex)
        {
            return ex.ToString();
        }
    }
}
