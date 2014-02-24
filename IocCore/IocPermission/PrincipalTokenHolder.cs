using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocPermission
{
    public static class PrincipalTokenHolder
    {
        private static IPrincipalToken currentPrincipal =new DefaultPrincipalToken();
        public static IPrincipalToken CurrentPrincipal
        {
            get
            {
                return currentPrincipal;
            }
            set
            {
                currentPrincipal = value;
            }
        }
    }
}
