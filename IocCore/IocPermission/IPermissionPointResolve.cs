using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocPermission
{
    public interface IPermissionPointResolve
    {
        PermissionInfo Resolve(PermissionPoint permissionPoint);
    }
}
