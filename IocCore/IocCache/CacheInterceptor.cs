using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Castle.Windsor;
using IocCore.Helper;
using System.Reflection;
using IocCore.IocDependency;
using IocCore.IocLog;

namespace IocCore.IocCache
{
    public class CacheInterceptor : IocInterceptor
    {
        private IDependencyWrapper getDependency(IInvocation invocation, CacheAttribute attribute)
        {
            
                Type targetType = invocation.TargetType;
                MethodInfo dependencyCallback = targetType.GetMethod(attribute.DependencyCallback);
                IDependencyWrapper dependency = (IDependencyWrapper)dependencyCallback.Invoke(
                    null,
                    new object[]{
                        InterceptorHelper.GetInvocationTarget(invocation),
                        InterceptorHelper.GetInvocationMethodArgs(invocation)
                    }
                );
                return dependency;
           
        }
        private string getKey(IInvocation invocation, CacheAttribute attribute)
        {
            if (attribute.IsKey)
            {
                if (attribute.IsResolve)
                {
                    throw new NotSupportedException();
                }
                else
                {
                    return attribute.Key;
                }
            }
            else//if (attribute.IsArg)
            {
                if (attribute.IsResolve)
                {
                    throw new NotSupportedException();
                }
                else
                {
                    object[] args = InterceptorHelper.GetInvocationMethodArgs(invocation);
                    if (attribute.Arg <= args.Length)
                    {
                        return args[attribute.Arg].ToString();
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
            }
        }
        public override void Intercept(IInvocation invocation)
        {
            CacheAttribute attribute = AttributeHelper.GetAttribute<CacheAttribute>(invocation) as CacheAttribute;
            attribute.Key = getKey(invocation, attribute);
            ICache cache = IocCoreFactory.Get<ICache>();
            object cacheData = cache.Get(attribute.Key);
            if (cacheData != null)
            {
                invocation.ReturnValue = cacheData;
                if (LogLevel <= attribute.LogLevel)
                {
                    StringBuilder logstr = new StringBuilder();
                    logstr.AppendFormat("Cache Hit! Key:\"{0}\" Caller:{1}", attribute.Key, InterceptorHelper.GetMethodInfo(invocation));
                    Log.Log(logstr.ToString(), attribute.LogLevel);
                }
                return;
            }
            IDependencyWrapper dependency;
            if (!String.IsNullOrWhiteSpace(attribute.DependencyCallback))
                dependency = getDependency(invocation, attribute);
            else
                dependency = null;
            invocation.Proceed();
            if (dependency != null)
            {
                object returnValue = invocation.ReturnValue;
                MethodInfo onRemovedCallback = invocation.TargetType.GetMethod(attribute.OnRemovedCallback);
                cache.Insert(
                    attribute.Key,
                    returnValue,
                    dependency.Instance,
                    attribute.Absolute,
                    attribute.Sliding,
                    (int)attribute.Priority,
                    onRemovedCallback);
                if (LogLevel <= attribute.LogLevel)
                {
                    StringBuilder logstr = new StringBuilder();
                    logstr.AppendFormat("Cache Insert! Key:\"{0}\" Caller:{1}", attribute.Key, InterceptorHelper.GetMethodInfo(invocation));
                    Log.Log(logstr.ToString(), attribute.LogLevel);
                }
            }
        }
    }
}
