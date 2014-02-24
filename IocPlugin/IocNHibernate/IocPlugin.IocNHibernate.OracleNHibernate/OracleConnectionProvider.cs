using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Connection;
using IocCore.IocDbOp;

namespace IocPlugin.IocNHibernate.OracleNHibernate
{
    //一个SessionFactory实例只有一个OracleConnectionProvider，不同的session共享一个连接对象
    public class OracleConnectionProvider : IConnectionProvider
    {
        public static IDbOp op = IocCore.IocCoreFactory.Get<IDbOp>();
        public virtual void CloseConnection(System.Data.IDbConnection conn)
        {
            //由于每次实际执行sql语句完成后都将调用这里来关闭连接，从而导致数据库缓存依赖失效
            //故这里不关闭，SessionFactory.Dispose之前连接将一直保持连接状态
            //conn.Close();
            //Console.WriteLine("Connection closed!");
        }
        public void Configure(IDictionary<string, string> settings)
        {
            op.ConnectString = settings["connection.connection_string"];
        }

        
        public virtual System.Data.IDbConnection GetConnection()
        {
            op.Open();
            return op.GetConnection();
        }

        public void Dispose()
        {
            op.Dispose();
        }

        private NHibernate.Driver.IDriver driver = new NHibernate.Driver.OracleDataClientDriver();
        public NHibernate.Driver.IDriver Driver
        {
            get { return driver; }
        }
    }
}
