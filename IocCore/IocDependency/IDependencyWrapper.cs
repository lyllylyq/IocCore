using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IocCore.IocDependency.CreateArgs;

namespace IocCore.IocDependency
{
    public interface IDependencyWrapper
    {
        /// <summary>
        /// 生成依赖对象。
        /// </summary>
        void Create(DependencyCreateArgs args);
        /// <summary>
        /// 获取或设置依赖对象。
        /// </summary>
        object Instance { get; }
        ///// <summary>
        ///// 获取一个值，该值指示 Dependency 对象是否已更改。
        ///// </summary>
        ///// <returns>Dependency 对象已更改，则为 true；否则为 false。默认值为 false。</returns>
        //public bool HasChanged { get; }
        ///// <summary>
        ///// 获取依赖项的上次更改时间。
        ///// </summary>
        ///// <returns>依赖项的上次更改时间。</returns>
        //public DateTime UtcLastModified { get; }
        ///// <summary>
        ///// 释放使用的资源。
        ///// </summary>
        //public void Dispose();
        ///// <summary>
        ///// 检索 Dependency 对象的唯一标识符。
        ///// </summary>
        ///// <returns>Dependency 对象的唯一标识符</returns>
        //public virtual string GetUniqueID();
        ///// <summary>
        ///// 通知 Dependency 对象表示的依赖项已更改。
        ///// </summary>
        ///// <param name="sender"> 事件源</param>
        ///// <param name="e"> 包含事件数据的 System.EventArgs 对象。</param>
        ///// <returns>Dependency 对象的唯一标识符</returns>
        //public void NotifyDependencyChanged(object sender, EventArgs e);
        
    }
}
