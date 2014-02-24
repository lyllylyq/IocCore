using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.Web.Caching;
using Oracle.DataAccess.Client;
using System.Data.Common;
using IocCore.IocDependency;
using IocCore.IocDependency.CreateArgs;
namespace IocPlugin.IocDbOp.OracleOp
{
    public class OracleCacheDependencyWrapper:IDependencyWrapper
    {
        private OracleCacheDependency instance= null;
        public void Create(DependencyCreateArgs args)
        {
            DBDependencyCreateArgs arg=(DBDependencyCreateArgs)args;
            if (args == null)
                throw new ArgumentException();
            OracleConnection conn = (OracleConnection)arg.DbOp.GetConnection();
            using (OracleCommand cmd = new OracleCommand(arg.Sql, conn))
            {
                instance = new OracleCacheDependency(cmd); 
            }
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
