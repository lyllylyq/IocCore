using System;
using System.Collections.Generic;
using System.Linq;
namespace IocPlugin.IocDbOp.OracleOp
{
    /// <summary>
    /// 提供Oracle操作的单例类
    /// 需要在配置文件中设置ConnectString或在第一次获取单例对象时立即设置ConnectString，两种方法二选一
    /// 获得ConnectString后立即与数据库连接，一般不需要手动Open、Close及Dispose
    /// 注意手动Close和Dispose将使依赖于此连接的缓存立即失效
    /// </summary>
    public class OracleSingletonOp : OracleOp
    {
        /// <summary>
        /// 如果配置文件中设置了ConnectString的值，则首次获得操作类对象时将被默认与数据库进行连接
        /// 如果没有在配置文件中设置ConnectString，则在首次为ConnectString赋值时立即默认连接数据库
        /// 一旦与数据库连接成功，只有先Dispose才能更改连接字符串
        /// </summary>
        public override string ConnectString
        {
            set
            {
                if (conn != null) throw new NotSupportedException("手动Dispose后才能更改ConnectString");
                connectString = value;
                Open();
            }
            get { return connectString; }
        }
    }
}
