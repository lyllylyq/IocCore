using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocPermission
{
    /// <summary>
    /// 访问决定者，用于判断访问权限的核心接口
    /// </summary>
    /// <author>vincent valenlee</author>
    public interface IAccessDecider
    {
        /// <summary>
        /// 决定指定的身份是否能对指定资源进行访问
        /// </summary>
        /// <param name="principal">指定的身份令牌</param>
        /// <param name="cntext">请求访问的对象</param>
        /// <exception cref="AccessException">如果不允许访问资源则抛出</exception>
        void Decide(IPrincipalToken principal, object cntext);

    }
}
