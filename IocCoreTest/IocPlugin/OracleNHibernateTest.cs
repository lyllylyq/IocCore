using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using System.Data;
using IocCoreTest.IocPlugin.Helper;
using IocCore;
using IocCore.IocDbOp;
using IocCore.IocCache;
using IocCore.IocDependency;
using IocCore.IocDependency.CreateArgs;
using IocPlugin.IocNHibernate.OracleNHibernate;
using IocCore.IocLog;
namespace IocCoreTest.IocPlugin
{
    [TestFixture]
    public class OracleNHibernateTest
    {
        public class SimpleBussiness
        {
            public virtual IList<CITY> TestGetData()
            {
                using (ISession session = OracleSessionFactory.GetInstance().OpenSession())
                {
                    IQuery sessionCreateQuery = session.CreateQuery("from CITY");
                    IList<CITY> pList = sessionCreateQuery.List<CITY>();
                    return pList;
                }
            }
            [Cache(KeyType = KeyType.str, Key = "select * from city", LogLevel = LogLevel.debug,
                DependencyCallback = "CallbackDef.DbCacheDependencyCallback",
                OnRemovedCallback = "CallbackDef.OnCacheRemovedCallback")]
            public virtual IList<CITY> TestCacheGetData(IDbOp op)
            {
                using (ISession session = OracleSessionFactory.GetInstance().OpenSession())
                {
                    IQuery sessionCreateQuery = session.CreateQuery("from CITY");
                    IList<CITY> pList = sessionCreateQuery.List<CITY>();
                    return pList;
                }
            }
            
        }


        [Test]
        public void TestConnection()
        {
            using (ISession session = OracleSessionFactory.GetInstance().OpenSession())
            {
                ConnectionState state = session.Connection.State;
                Assert.AreEqual(session.Connection.State, state);
            }
        }
    }
}
