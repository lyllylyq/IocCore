using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IocCore.IocDependency;
using IocCore.IocCache;
using IocCore.IocDependency.CreateArgs;
using IocCore;
using IocCore.IocDbOp;

namespace IocCoreTest.IocPlugin.Helper
{
    public class CallbackDef
    {
        public static void OnCacheRemovedCallback(string key, object value, RemovedReason reason)
        {
            Console.WriteLine(String.Format("CacheItemRemoved key:{0} reason:{1}", key, reason));
        }
        public static IDependencyWrapper DbCacheDependencyCallback(object target, object[] args)
        {
            CacheDependencyWrapper dependency = new CacheDependencyWrapper();
            try
            {
                if (args.Length == 2)
                {
                    IDependencyWrapper dbDependency = IocCoreFactory.Get<IDependencyWrapper>("DBDependency");
                    DBDependencyCreateArgs dbArg
                        = new DBDependencyCreateArgs() { Sql = args[0] as string, DbOp = args[1] as IDbOp };
                    dbDependency.Create(dbArg);
                    IDependencyWrapper dbConnDependency = IocCoreFactory.Get<IDependencyWrapper>("DBConnDependency");
                    DBConnDependencyCreateArgs dbCoonArg
                        = new DBConnDependencyCreateArgs() { DbOp = args[1] as IDbOp };
                    dbConnDependency.Create(dbCoonArg);
                    IDependencyWrapper aggregateDependency = IocCoreFactory.Get<IDependencyWrapper>("AggregateDependency");
                    AggregateDependencyCreateArgs aggregateArg
                        = new AggregateDependencyCreateArgs() { Wrappers = new IDependencyWrapper[] { dbDependency, dbConnDependency } };
                    aggregateDependency.Create(aggregateArg);
                    return aggregateDependency;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception ex)
            {
                //这里处理缓存依赖生成过程中的异常
                throw ex;
                //return null;
            }

        }
    }
}
