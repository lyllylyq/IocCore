using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocDependency
{
    public class EmptyDependencyWrapper:IDependencyWrapper
    {
        public void Create(CreateArgs.DependencyCreateArgs args)
        {
            throw new NotImplementedException();
        }

        public object Instance
        {
            get { throw new NotImplementedException(); }
        }
    }
}
