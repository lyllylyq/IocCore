using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace NHibernateCacheTest
{
    public class SessionFactory
    {
        private static ISessionFactory sessionFactory = null;
        public static ISessionFactory GetInstance()
        {
            if(sessionFactory==null)
                sessionFactory = new Configuration().Configure("hibernateOracle.cfg.xml").BuildSessionFactory();
            return sessionFactory;
        }
        public static void Dispose()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
                sessionFactory.Dispose();
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
