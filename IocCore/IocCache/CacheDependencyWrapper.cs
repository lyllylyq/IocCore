using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocCache
{
    /// <summary>
    /// 缓存依赖的包装类
    /// 缓存容器可能对缓存依赖的类型有特殊要求，而缓存拦截器是不知道这些信息的
    /// 所以使用这个包装类对类型信息进行封装
    /// </summary>
    public class CacheDependencyWrapper
    {
        private object instance = null;
        /// <summary>
        /// 
        /// </summary>
        public object Instance 
        {
            get
            {
                if (instance == null)
                    return emptyDependency;
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private static readonly EmptyDependency emptyDependency = new EmptyDependency();
        private class EmptyDependency { }
        
    }
}
