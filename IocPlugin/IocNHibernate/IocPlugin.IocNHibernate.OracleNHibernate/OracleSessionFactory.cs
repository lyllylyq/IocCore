using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using IocCore.IocDbOp;

namespace IocPlugin.IocNHibernate.OracleNHibernate
{
    public class OracleSessionFactory 
    {
        private static ISessionFactory sessionFactory = null;
        
        public static IDbOp DbOp
        {
            get
            {
                return OracleConnectionProvider.op;
            }
        }
        public static ISessionFactory GetInstance()
        {
            if (sessionFactory == null)
                sessionFactory = new Configuration().Configure("ClientBin\\hibernateOracle.cfg.xml").BuildSessionFactory();
            return sessionFactory;
        }
        public static void Dispose()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }
        /// <summary>
        /// 获取session
        /// </summary>
        /// <returns></returns>
        public static ISession GetSession()
        {
            try
            {
                ISession pSession = sessionFactory.OpenSession();
                return pSession;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
