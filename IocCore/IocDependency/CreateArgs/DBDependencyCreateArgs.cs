using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IocCore.IocDbOp;

namespace IocCore.IocDependency.CreateArgs
{
    public class DBDependencyCreateArgs:DependencyCreateArgs
    {
        public string Sql
        {
            get
            {
                object value=getArg("Sql");
                if(value!=null)
                    return value.ToString();
                throw new ArgumentNullException();
            }
            set
            {
                addArg("Sql", value);
            }
        }
        public IDbOp DbOp
        {
            get
            {
                object value = getArg("DbOp");
                if (value != null)
                    return (IDbOp)value;
                throw new ArgumentNullException();
            }
            set
            {
                addArg("DbOp", value);
            }
        }
    }
}
