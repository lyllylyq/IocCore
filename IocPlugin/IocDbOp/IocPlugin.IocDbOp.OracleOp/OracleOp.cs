using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.DataAccess.Client;
using System.Data;
using System.Data.Common;
using IocCore.IocCache;
using IocCore.IocDbOp;
namespace IocPlugin.IocDbOp.OracleOp
{
    /// <summary>
    /// 提供Oracle操作的普通类
    /// 获取对象后需手动Open、Close及Dispose
    /// </summary>
    public class OracleOp:IDbOp
    {
        protected string connectString;
        /// <summary>
        /// 可以在配置文件中为所有实例设置默认ConnectString
        /// 设置ConnectString会导致已有连接对象Dispose，设置后需要手动Open
        /// </summary>
        public virtual string ConnectString
        {
            set 
            {
                if(conn!=null)
                    Dispose();
                connectString = value;
            }
            get { return connectString; }
        }
        protected OracleConnection conn = null;
        ~OracleOp()
        {
            Dispose();
        }

        protected readonly CacheDependentable connectionState = new CacheDependentable();
        public virtual IDependentable ConnectionState
        {
            get { return connectionState; }
        }

        public DbConnection GetConnection()
        {
            return conn;
        }
        /// <summary>
        /// 关闭连接，将导致依赖此连接的缓存立即失效
        /// </summary>
        public virtual void Close()
        {
            if (conn != null&&conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
                Console.WriteLine("Connection closed!");
                ConnectionState.DependentableChanged(this, EventArgs.Empty);
            }
            
        }
        /// <summary>
        /// 销毁连接，将导致依赖此连接的缓存立即失效
        /// </summary>
        public virtual void Dispose()
        {
            if (conn != null)
            {
                Close();
                conn.Dispose();
                conn = null;
                Console.WriteLine("Connection disposed!");
            }
        }
        public virtual void Open()
        {
            if (conn == null)
            {
                conn = new OracleConnection(ConnectString);
            }
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
                Console.WriteLine("Connection opened!");
            }
        }

        public virtual DataSet Excute(string sql)
        {
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                DataSet ds = new DataSet();
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                oda.Fill(ds, "Table");
                return ds;
            }
        }
    }
}
