using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using System.Reflection;

namespace IocCore.Helper
{
    public class InterceptorHelper
    {
        //生成方法调用信息的字符串
        public static string GetMethodInfo(IInvocation invocation)
        {
            StringBuilder infostr = new StringBuilder();
            infostr.AppendFormat("{0}:{1}(", invocation.TargetType, invocation.Method.Name);
            if (invocation.Arguments != null && invocation.Arguments.Length > 0)
            {
                for (int i = 0; i < invocation.Arguments.Length; i++)
                {
                    if (i != 0) infostr.Append(", ");
                    if (invocation.Arguments[i] == null)
                    {
                        infostr.Append("null");
                    }
                    else
                    {
                        infostr.Append(invocation.Arguments[i].GetType() == typeof(string)
                           ? String.Format("\"{0}\"", invocation.Arguments[i])
                           : invocation.Arguments[i].ToString());
                    }
                }
            }
            infostr.Append(")");
            return infostr.ToString();
        }

        public static Object GetInvocationTarget(IInvocation invocation)
        {
            return invocation.InvocationTarget;
        }
        public static MethodInfo GetInvocationMethod(IInvocation invocation)
        {
            return invocation.MethodInvocationTarget;
        }
        public static object[] GetInvocationMethodArgs(IInvocation invocation)
        {
            return invocation.Arguments;
        }
    }
}
