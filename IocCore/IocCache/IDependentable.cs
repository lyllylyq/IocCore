using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace IocCore.IocCache
{
    public interface IDependentable
    {
        void RegisterDependency(EventHandler callback);
        void DependentableChanged(object sender, EventArgs e);
        void UnRegisterAllDependency();
    }
}
