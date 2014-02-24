using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocPermission
{
    public class DefaultPermissionPointResolve : IPermissionPointResolve
    {
        public PermissionInfo Resolve(PermissionPoint permissionPoint)
        {
            DefaultPermissionInfo info = new DefaultPermissionInfo(permissionPoint.Name, permissionPoint.Action);
            return info;
        }
    }
}
