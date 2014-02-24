using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Data;

using IocCore;
using IocCore.IocDbOp;
using IocCore.IocCache;
using IocCore.IocDependency;
using IocCore.IocDependency.CreateArgs;
using IocCore.IocPermission;
using IocCore.IocLog;
using IocCore.IocExHandle;
using NHibernateCacheTest;
using NHibernate;
using IocPlugin.IocNHibernate.OracleNHibernate;
namespace IocDemo
{
    public class NHCacheTest
    {
        [Log(LogLevel = LogLevel.debug, Option = LogOption.all)]
        [ExHandle(Option = ExHandleOption.log | ExHandleOption.ignore, LogLevel = LogLevel.debug)]
        [Cache(KeyType = KeyType.str, Key="select * from city", LogLevel = LogLevel.debug,
            DependencyCallback = "TestGetData_CacheDependencyCallback",
            OnRemovedCallback = "TestGetData_OnRemovedCallback")]
        public virtual IList<CITY> TestGetData(string sql, IDbOp op)
        {
            ISession session = OracleSessionFactory.GetInstance().OpenSession(new TestInterceptor());
            IQuery sessionCreateQuery = session.CreateQuery("from CITY");
            IList<CITY> pList = sessionCreateQuery.List<CITY>();
            return pList;
        }
        public static void TestGetData_OnRemovedCallback(string key, object value, RemovedReason reason)
        {
            Console.WriteLine(String.Format("CacheItemRemoved key:{0} reason:{1}", key, reason));
        }
        public static IDependencyWrapper TestGetData_CacheDependencyCallback(object target, object[] args)
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

    public class CacheTest
    {
        [Log(LogLevel = LogLevel.debug, Option = LogOption.all)]
        [ExHandle(Option=ExHandleOption.log|ExHandleOption.ignore ,LogLevel=LogLevel.debug)]
        [PermissionPoint(Action = "action1", Name = "name1", Resource = "Table",
            DenyHandle = DenyHandle.alertAndThrow,
            LogOpt = LogOpt.all, LogLevel = LogLevel.debug,
             ResolveType = "DefaultPermissionPointResolve")]
        [Cache(KeyType = KeyType.arg, Arg = 0, LogLevel = LogLevel.debug, 
            DependencyCallback = "TestGetData_CacheDependencyCallback",
            OnRemovedCallback = "TestGetData_OnRemovedCallback")]
        public virtual DataSet TestGetData(string sql, IDbOp op)
        {
            DataSet ds = op.Excute("select * from bpxm t");
            return ds;
        }
        public static void TestGetData_OnRemovedCallback(string key, object value, RemovedReason reason)
        {
            Console.WriteLine(String.Format("CacheItemRemoved key:{0} reason:{1}", key, reason));
        }
        public static IDependencyWrapper TestGetData_CacheDependencyCallback(object target, object[] args)
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

    class Program
    {
        
        static void Main(string[] args)
        {
            ISession session = OracleSessionFactory.GetInstance().OpenSession(new TestInterceptor());
            NHCacheTest test = IocCoreFactory.Get<NHCacheTest>();
            test.TestGetData("select * from city", OracleSessionFactory.DbOp);
            test.TestGetData("select * from city", OracleSessionFactory.DbOp);
            //IQuery sessionCreateQuery = session.CreateQuery("from CITY");
            //IList<CITY> pList = sessionCreateQuery.List<CITY>();
            //Console.WriteLine(pList.Count);
            //Console.ReadLine();
            ////session = SessionFactory.GetInstance().OpenSession(new TestInterceptor());
            ////sessionCreateQuery = session.CreateQuery("from CITY");
            
            //session.Evict(pList);
            //pList = sessionCreateQuery.List<CITY>();
            //session.Close();
            Console.ReadLine();
            OracleSessionFactory.Dispose();
            Console.ReadLine();
            #region
            //IDbOp op = IocCoreFactory.Get<IDbOp>();
            //op.Open();
            //CacheTest test = IocCoreFactory.Get<CacheTest>();
            //test.TestGetData("select * from bpxm t",op);
            //test.TestGetData("select * from bpxm t", op);
            ////op.Close();
            ////op.Open();
            ////Thread.Sleep(2000);
            ////test.TestGetData("select * from bpxm t", op);
            ////Console.ReadLine();
            ////test.TestGetData("select * from bpxm t", op);
            
            ////Console.ReadLine();
            //op.Close();
            //Console.ReadLine();
            #endregion
            #region
            //IDbOp op = IocCoreFactory.Get<IDbOp>();
            //bool a = false;
            //ConsoleKeyInfo keyInfo;
            //while (!a)
            //{
            //    if (System.Console.KeyAvailable)
            //    {
            //        keyInfo = System.Console.ReadKey(true);
            //        if (keyInfo.KeyChar == 'a')
            //            a = true;
            //    }
            //    op.Open();
            //    op.CacheQuery("select * from bpxm t");
            //    Thread.Sleep(2000);
            //}
            #endregion
        }
    }
}
