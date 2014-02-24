using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace IocCore.IocCache
{
    public interface ICache
    {
        void Insert(string key, object value);
        void Insert(string key, object value, object dependencies, int absoluteExpiration, int slidingExpiration, int priority, MethodInfo onRemovedCallback);
        object Get(string key);
        object Remove(string key);
    }
}
