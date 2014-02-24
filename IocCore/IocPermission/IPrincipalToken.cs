using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocPermission
{
    /// <summary>
    /// 标识访问资源的一个身份
    /// </summary>
    /// <author>vincent valenlee</author>
    public interface IPrincipalToken
    {
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 身份凭证
        /// </summary>
        object Certificate
        {
            get;
            set;
        }

        /// <summary>
        /// 身份的其他信息，如邮件地址等
        /// </summary>
        object Information
        {
            get;
            set;
        }

        /// <summary>
        /// 获取身份的授权集
        /// </summary>
        PermissionInfoCollection GetGrandedPermission();

    }
}
