using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocPermission
{
    public class DefaultAccessDecider:IAccessDecider
    {
        public void Decide(IPrincipalToken principal, object cntext)
        {
            if (!principal.GetGrandedPermission().Contains(cntext as PermissionInfo)) 
            {
                AccessException ex = new AccessException("无权限") { CheckObject = cntext };
                throw ex;
            }
        }
    }
}
