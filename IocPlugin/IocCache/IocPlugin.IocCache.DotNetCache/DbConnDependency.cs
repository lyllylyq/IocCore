using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;

namespace IocPlugin.IocCache.DotNetCache
{
    public class DbConnDependency:CacheDependency
    {
        public void NotifyDependencyChanged(object sender, EventArgs e)
        {
            base.NotifyDependencyChanged(sender, e);
        }
        
    }
}
