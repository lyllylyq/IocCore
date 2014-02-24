using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocCache
{
    public class CacheDependentable:IDependentable
    {
        public event EventHandler DependentableChangedHandler;
        public void RegisterDependency(EventHandler callback)
        {
            DependentableChangedHandler += callback;
        }
        public void DependentableChanged(object sender,EventArgs e)
        {
            if (DependentableChangedHandler != null)
                DependentableChangedHandler(sender, e);
            UnRegisterAllDependency();
        }

        public void UnRegisterAllDependency()
        {
            DependentableChangedHandler = null;
        }
    }
}
