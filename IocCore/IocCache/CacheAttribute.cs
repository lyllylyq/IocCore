using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IocCore.IocDependency;
using IocCore.IocLog;

namespace IocCore.IocCache
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : Attribute
    {
        /// <summary>
        /// 缓存Key值的指定方式,必须指定
        /// str为指定静态字符串，arg为指定方法的某个参数
        /// </summary>
        public virtual LogLevel LogLevel { get; set; }
        public virtual KeyType KeyType { get; set; }
        public virtual string Key { get; set; }
        public virtual int Arg { get; set; }
        public virtual bool Resolve { get; set; }
        public virtual string ResolveType { get; set; }
        public virtual int Absolute { get; set; }
        public virtual int Sliding { get; set; }
        private CachePriority priority = CachePriority.Normal;
        public virtual CachePriority Priority { get { return priority; } set { priority = value; } }

        public delegate IDependencyWrapper CacheDependencyCallback(object thisObject, object[] args);
        public string DependencyCallback { get; set; }
        public delegate void CacheItemRemovedCallback(string key, object value, RemovedReason reason);
        public string OnRemovedCallback { get; set; }

        public bool IsKey { get { return KeyType == KeyType.str; } }
        public bool IsArg { get { return KeyType == KeyType.arg; } }
        public bool IsResolve { get { return Resolve; } }
    }
}
