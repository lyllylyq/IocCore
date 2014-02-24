using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocDependency.CreateArgs
{
    public class AggregateDependencyCreateArgs : DependencyCreateArgs
    {
        public IDependencyWrapper[] Wrappers
        {
            get
            {
                object value = getArg("Wrappers");
                if (value != null)
                    return (IDependencyWrapper[])value;
                throw new ArgumentNullException();
            }
            set
            {
                addArg("Wrappers", value);
            }
        }
    }
}
