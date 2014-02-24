using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocPermission
{
    public class DefaultPermissionInfo : PermissionInfo
    {
        public DefaultPermissionInfo(string name, string action)
            : base(name, action)
        {
        }
        public DefaultPermissionInfo(string name)
            : base(name)
        {
            
        }
        public DefaultPermissionInfo()
        {
            
        }
         

        public override bool Contains(PermissionInfo permission)
        {
            DefaultPermissionInfo tp = permission as DefaultPermissionInfo;
            if (tp == null)
                return false;
            if (!CompareName(tp))
            {
                return false;
            }
            else
            {
                return CompareAction(tp);
            }
        }

        private bool CompareName(DefaultPermissionInfo permission)
        {
            if (this.Name == null)
                return permission.Name == null;
            else
                return this.Name.Equals(permission.Name);
        }

        private bool CompareAction(DefaultPermissionInfo permission)
        {
            if (this.Action == null)
                return permission.Action == null;
            else
                return this.Action.Equals(permission.Action);
        }
    }
}
