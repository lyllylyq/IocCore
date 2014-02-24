using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace IocCore.IocPermission
{
    public class DefaultPermissionPoint : PermissionPoint
    {
        public DefaultPermissionPoint()
        {
            
        }
        public DefaultPermissionPoint(PermissionPointAttribute attribute, object context, MemberInfo member, object[] args)
            : base(attribute, context, member, args)
        {
            
        }
        public override PermissionInfo NewPermission()
        {
            return new DefaultPermissionInfo(Name, Action);
        }
    }
}
