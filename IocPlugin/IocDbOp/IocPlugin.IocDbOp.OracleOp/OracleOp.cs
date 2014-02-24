using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IocCore.IocLog;
using IocCore.IocExHandle;
using IocCore.IocPermission;
using Oracle.DataAccess.Client;
using Oracle.Web.Caching;
using System.Data;
using System.Web;
using System.Data.Common;
using IocCore.IocCache;
using IocCore.IocDbOp;
namespace IocPlugin.IocDbOp.OracleOp
{
    public class OracleOp:IDbOp
    {
        private string connectString = "Data Source=192.9.100.110/orcl; User Id=GDYW; Password=sjzx";
        public virtual string ConnectString
        {
            set { connectString = value; }
            get { return connectString; }
        }
        private OracleConnection conn = null;

        private readonly CacheDependentable connectionState = new CacheDependentable();
        public virtual IDependentable ConnectionState
        {
            get { return connectionState; }
        }

        public DbConnection GetConnection()
        {
            return conn;
        }
        public virtual void Close()
        {
            if (conn != null&&conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
                Console.WriteLine("Connection closed!");
                ConnectionState.DependentableChanged(this, EventArgs.Empty);
            }
            
        }
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
