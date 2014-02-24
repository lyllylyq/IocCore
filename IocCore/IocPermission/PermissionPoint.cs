using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace IocCore.IocPermission
{
    public abstract class PermissionPoint
    {
        public PermissionPoint() { }
        public PermissionPoint(PermissionPointAttribute attribute, object context, MemberInfo member, object[] args)
        {
            name = attribute.Name;
            resource = attribute.Resource;
            action = attribute.Action;
            Context = context;
            Member = member;
            Args = args;
        }
        private string name;
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string resource;
        public virtual string Resource
        {
            get { return resource; }
            set { resource = value; }
        }

        private string action;
        public virtual string Action
        {
            get { return action; }
            set { action = value; }
        }

        //定义权限点的对象
        public object Context { get; set; }

        //定义的字段、属性、方法、事件等
        public MemberInfo Member { get; set; }

        //执行方法时的参数列表
        public object[] Args { get; set; }

        public abstract PermissionInfo NewPermission();

        public static PermissionPoint EMPTY_PERMISSION_POINT = new EmptyPermissionPoint();
    }
    public class EmptyPermissionPoint : PermissionPoint
    {
        public override string Name
        {
            get { return ""; }
        }

        public override string Resource
        {
            get { return ""; }
        }

        public override string Action
        {
            get { return ""; }
        }

        public override PermissionInfo NewPermission()
        {
            return PermissionInfo.EMPTY_PERMISSIONINFO;
        }
    }
}
