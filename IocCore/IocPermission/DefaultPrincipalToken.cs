using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocPermission
{
    public class DefaultPrincipalToken : IPrincipalToken
    {
        private string name;
        public string Name
        {
            get
            {
                return "admin";
            }
            set
            {
                name = value ;
            }
        }
        private string certificate;
        public object Certificate
        {
            get
            {
                return "Certificate";
            }
            set
            {
                certificate = value.ToString();
            }
        }
        private string information;
        public object Information
        {
            get
            {
                return "Information";
            }
            set
            {
                information = value.ToString();
            }
        }

        public PermissionInfoCollection GetGrandedPermission()
        {
            PermissionInfoCollection collection = new PermissionInfoCollection();
            collection.Add(new DefaultPermissionInfo("name1", "action1"));
            collection.Add(new DefaultPermissionInfo("name2", "action2"));
            return collection;
        }

        public override string ToString()
        {
            return String.Format("Name:{0}",Name);
        }
    }
}
