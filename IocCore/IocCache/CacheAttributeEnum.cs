using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocCache
{
    /// <summary>
    /// 缓存Key值的指定方式
    /// </summary>
    public enum KeyType
    {
        str,
        arg
    };
    /// <summary>
    /// 缓存项的相对优先级
    /// </summary>
    public enum CachePriority
    {
        Low = 1,
        BelowNormal = 2,
        Normal = 3,
        Default = 3,
        AboveNormal = 4,
        High = 5,
        NotRemovable = 6,
    }
    /// <summary>
    /// 缓存失效原因
    /// </summary>
    public enum RemovedReason
    {
        // 摘要:
        //     该项是通过指定相同键的 System.Web.Caching.Cache.Insert(System.String,System.Object)
        //     方法调用或 System.Web.Caching.Cache.Remove(System.String) 方法调用从缓存中移除的。
        Removed = 1,
        //
        // 摘要:
        //     从缓存移除该项的原因是它已过期。
        Expired = 2,
        //
        // 摘要:
        //     之所以从缓存中移除该项，是因为系统要通过移除该项来释放内存。
        Underused = 3,
        //
        // 摘要:
        //     从缓存移除该项的原因是与之关联的缓存依赖项已更改。
        DependencyChanged = 4,
    }
}
