using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocDependency.CreateArgs
{
    public abstract class DependencyCreateArgs
    {
        private Dictionary<string, object> args = new Dictionary<string, object>();
        protected void addArg(string key,object value)
        {
            args.Add(key, value);
        }
        protected object getArg(string key)
        {
            object value;
            if (!args.TryGetValue(key, out value))
                value = null;
            return value;
        }
    }
}
