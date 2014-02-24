using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using IocCore.Helper;
using IocCore.IocLog;
using System.Reflection;

namespace IocCore.IocPermission
{
    public class PermissionPointInterceptor : IocInterceptor
    {

        private bool tryAccess(IInvocation invocation)
        {
            PermissionPointAttribute attribute = AttributeHelper.GetAttribute<PermissionPointAttribute>(invocation) as PermissionPointAttribute;
            if (attribute == null) return true;
            string strLogHeader = "Access accepted";
            try
            {
                IPermissionPointResolve resolve = IocCoreFactory.Get<IPermissionPointResolve>(attribute.ResolveType);
                PermissionPoint point = new DefaultPermissionPoint(attribute,
                    InterceptorHelper.GetInvocationTarget(invocation),
                    InterceptorHelper.GetInvocationMethod(invocation) as MemberInfo,
                    InterceptorHelper.GetInvocationMethodArgs(invocation)) as PermissionPoint;
                PermissionInfo info= resolve.Resolve(point);
                info++;
                if (attribute.IsAcceptLog&&LogLevel <= attribute.LogLevel)
                {
                    StringBuilder logstr = new StringBuilder();
                    logstr.AppendFormat("{0} {1} {2} {3}-----Access Log ", strLogHeader, PrincipalTokenHolder.CurrentPrincipal.ToString(), attribute.ToString(), InterceptorHelper.GetMethodInfo(invocation));
                    Log.Log(logstr.ToString(), attribute.LogLevel);
                }
            }
            catch (AccessException ex)
            {
                strLogHeader = "Access Denied";
                if (attribute.IsAcceptLog && LogLevel <= attribute.LogLevel)
                {
                    StringBuilder logstr = new StringBuilder();
                    logstr.AppendFormat("{0} {1} {2} {3}-----Access Log ", strLogHeader, PrincipalTokenHolder.CurrentPrincipal.ToString(), attribute.ToString(), InterceptorHelper.GetMethodInfo(invocation));
                    Log.Log(logstr.ToString(), attribute.LogLevel, ex);

                }
                if (attribute.IsAlert)
                {
                    Console.WriteLine("Access diny alert!");
                }
                if (attribute.IsThrow)
                {
                    throw ex;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public override void Intercept(IInvocation invocation)
        {
            if (this.tryAccess(invocation)) 
                invocation.Proceed();
        }
    }
}
