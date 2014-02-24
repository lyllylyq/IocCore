using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocPermission
{
    /// <summary>
    /// 无授权时抛出的异常
    /// </summary>
    /// <author>vincent valenlee</author>
    public class AccessException : ApplicationException
    {
        //被检测的对象
        public object CheckObject { get; set; }

        public AccessException()
            : base("授权异常")
        {
        }

        public AccessException(string message)
            : base(message)
        {
        }
    }
}
