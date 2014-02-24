using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using IocCore.IocCache;

namespace IocCore.IocDbOp
{
    public interface IDbOp
    {
        DataSet Excute(string sql);
        void Close();
        void Open();
        void Dispose();
        DbConnection GetConnection();

        IDependentable ConnectionState { get; }
        string ConnectString { get; set; }
    }
}
