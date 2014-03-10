using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IocCore;
using IocCore.IocDbOp;
using System.Data;
namespace IocCoreTest.IocDbOp
{
    [TestFixture]
    public class OracleOpTest
    {
        public class SimpleBusiness
        {
            public IDbOp op = IocCoreFactory.Get<IDbOp>("OracleOp");

            public DataSet GetSomeData(string sql)
            {
                op.Open();
                DataSet ds=op.Excute(sql);
                op.Close();
                return ds;
            }
        }
        readonly SimpleBusiness business = new SimpleBusiness();
        public IDbOp op = IocCoreFactory.Get<IDbOp>("OracleOp");
        
        [Test]
        public void TestConnection()
        {
            op.Open();
            ConnectionState state = op.GetConnection().State;
            op.Close();
            Assert.AreEqual(ConnectionState.Open, state);
        }
        [Test]
        public void TestReConnection()
        {
            op.Dispose();
            op.ConnectString = "Data Source=localhost/orcl; User Id=javauser; Password=javauser";
            op.Open();
            ConnectionState state = op.GetConnection().State;
            op.Close();
            Assert.AreEqual(ConnectionState.Open, state);
        }
        [Test]
        public void TestReOpen()
        {
            op.Open();
            op.Close();
            op.Open();
            ConnectionState state = op.GetConnection().State;
            op.Close();
            Assert.AreEqual(ConnectionState.Open, state);
        }
        [Test]
        public void TestNotSingleton()
        {
            IDbOp op2 = IocCoreFactory.Get<IDbOp>("OracleOp");
            Assert.AreEqual(false, object.ReferenceEquals(op, op2));
        }
        [Test]
        public void TestExcute()
        {
            using (DataSet ds = business.GetSomeData("select * from city"))
            {
                Assert.AreEqual(true, (ds.Tables != null) && (ds.Tables[0].Rows.Count != 0));
            }
        }
    }
}
