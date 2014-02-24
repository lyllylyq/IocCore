using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Reflection;
using IocCore.IocCache;

namespace IocPlugin.IocCache.DotNetCache
{
    public class DotNetCache:ICache
    {
        public void Insert(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value);
        }
        public void Insert(string key, object value, object dependencies, int absoluteExpiration, int slidingExpiration, int priority, MethodInfo onRemovedCallback)
        {
            DateTime absolute;
            if (absoluteExpiration != 0)
                absolute = DateTime.Now.AddSeconds(absoluteExpiration);
            else
                absolute = Cache.NoAbsoluteExpiration;
            TimeSpan sliding;
            if (slidingExpiration != 0)
                sliding = TimeSpan.FromSeconds(slidingExpiration);
            else
                sliding = Cache.NoSlidingExpiration;
            HttpRuntime.Cache.Insert(
                key, 
                value, 
                (CacheDependency)dependencies,
                absolute,
                sliding,
                (CacheItemPriority)priority,
                (CacheItemRemovedCallback)Delegate.CreateDelegate(typeof(CacheItemRemovedCallback), onRemovedCallback)
                );
        }
        public object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        public object Remove(string key)
        {
            return HttpRuntime.Cache.Remove(key);
        }
    }
}
