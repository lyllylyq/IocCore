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
    public class OracleSingletonOpTest
    {
        public class SimpleBusiness
        {
            public IDbOp op = IocCoreFactory.Get<IDbOp>("OracleSingletonOp");
            public DataSet GetSomeData(string sql)
            {
                return op.Excute(sql);
            }
        }

        SimpleBusiness business=new SimpleBusiness();
        public IDbOp op = IocCoreFactory.Get<IDbOp>("OracleSingletonOp");
        
        [Test]
        public void TestConnection()
        {
            Assert.AreEqual(ConnectionState.Open, op.GetConnection().State);
        }
        [Test]
        public void TestReConnection()
        {
            op.Dispose();
            op.ConnectString = "Data Source=localhost/orcl; User Id=javauser; Password=javauser";
            Assert.AreEqual(ConnectionState.Open, op.GetConnection().State);
        }
        [Test]
        [ExpectedException(ExpectedException = typeof(NotSupportedException))]
        public void TestReConnectionException()
        {
            //op.Dispose();
            op.ConnectString = "Data Source=localhost/orcl; User Id=javauser; Password=javauser";
        }
        [Test]
        public void TestReOpen()
        {
            op.Close();
            op.Open();
            Assert.AreEqual(ConnectionState.Open, op.GetConnection().State);
        }
        [Test]
        public void TestSingleton()
        {
            IDbOp op2 = IocCoreFactory.Get<IDbOp>("OracleSingletonOp");
            Assert.AreEqual(true, object.ReferenceEquals(op, op2));
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
