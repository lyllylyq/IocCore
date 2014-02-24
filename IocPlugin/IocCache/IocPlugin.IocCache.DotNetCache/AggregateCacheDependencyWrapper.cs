using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IocCore.IocDependency;
using IocCore.IocDependency.CreateArgs;
using System.Web.Caching;

namespace IocPlugin.IocCache.DotNetCache
{
    public class AggregateCacheDependencyWrapper : IDependencyWrapper
    {
        private AggregateCacheDependency instance = null;
        public void Create(DependencyCreateArgs args)
        {
            AggregateDependencyCreateArgs arg = (AggregateDependencyCreateArgs)args;
            IDependencyWrapper[] wrappers = arg.Wrappers;
            instance = new AggregateCacheDependency();
            foreach (IDependencyWrapper wrapper in wrappers)
            {
                instance.Add((CacheDependency)wrapper.Instance);
            }
        }

        public object Instance
        {
            get { return instance; }
        }
    }
}
