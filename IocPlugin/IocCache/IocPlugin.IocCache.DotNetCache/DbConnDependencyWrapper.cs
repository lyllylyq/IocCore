using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IocCore.IocDependency;
using IocCore.IocDependency.CreateArgs;
using System.Web.Caching;
using IocCore.IocDbOp;

namespace IocPlugin.IocCache.DotNetCache
{
    public class DbConnDependencyWrapper : IDependencyWrapper
    {
        private DbConnDependency instance =null;
        public void Create(DependencyCreateArgs args)
        {
            instance = new DbConnDependency();
            DBConnDependencyCreateArgs arg = (DBConnDependencyCreateArgs)args;
            if (arg == null)
                throw new ArgumentException();
            IDbOp op = arg.DbOp;
            op.ConnectionState.RegisterDependency(DependentableChangedCallback);
        }
        public void DependentableChangedCallback(object sender, EventArgs e)
        {
            instance.NotifyDependencyChanged(sender, e);
        }
        public object Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
