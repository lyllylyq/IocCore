using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;

namespace IocCore.Helper
{
    class AttributeHelper
    {
        //从拦截对象中获取标签
        public static Attribute GetAttribute<T>(IInvocation invocation)
        {
            Attribute attribute;
            //尝试获取method级特性
            Object[] atts = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(T), true);
            if (atts.GetLength(0) > 0)
                attribute = atts[0] as Attribute;
            else
                attribute = null;
            //尝试获取class级特性
            if (attribute == null)
            {
                System.Reflection.MemberInfo classInfo = invocation.InvocationTarget.GetType();
                attribute = Attribute.GetCustomAttribute(classInfo, typeof(T));
            }
            return attribute;
        }
    }
}
