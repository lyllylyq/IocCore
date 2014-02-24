using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IocCore.IocPermission
{
    /// <summary>
    /// 封装权限的信息，权限信息具有一个名称以及内容字符。内容字符的格式，由实现决定。
    /// 权限是具有包含关系的，例如edit编辑权限就包含了view查看权限。是否具有包含关系，也由实现决定。
    /// 权限信息是可以合并的,可以将同类型的小权限合并为一个大权限
    /// </summary>
    /// <author>vincent valenlee</author>
    [Serializable]
    public abstract class PermissionInfo : ICloneable
    {

        protected string name;

        public virtual string Name
        {
            get { return name; }
        }

        protected string action;

        public virtual string Action
        {
            get { return action; }
        }

        public PermissionInfo(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// 此构造在子类中必须重载
        /// </summary>
        public PermissionInfo(string name, string action)
        {
            this.name = name;
            this.action = action;
        }

        public PermissionInfo()
            : this("", "")
        {
        }

        public override bool Equals(object other)
        {
            if (other == this)
                return true;
            if (other is EmptyPermissionInfo)
                return true;//所有权限都包含空权限！！！！
            if (other is PermissionInfo)
            {
                PermissionInfo p = (PermissionInfo)other;
                return Name.Equals(p.name) && Action.Equals(p.action);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Action.GetHashCode();
        }

        /// <summary>
        /// 判断此权限是否包含指定的权限
        /// </summary>
        /// <returns>如果此权限包含指定权限则返回true</returns>
        public abstract bool Contains(PermissionInfo permission);

        /// 移植到工厂中进行
        //public abstract PermissionInfo ResolvePermission(string permission);

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// 空权限信息，此权限信息表示不能访问任何授权资源，但被所有其他权限包含（不做权限检查）
        /// </summary>
        public class EmptyPermissionInfo : PermissionInfo
        {

            public override bool Contains(PermissionInfo permission)
            {
                return false;
            }
        }

        /// <summary>
        /// 权限判断操作符，系统支持使用++操作符进行判断当前用户是否具有此权限
        /// </summary>
        public static PermissionInfo operator ++(PermissionInfo pinfo)
        {
            //判断当前用户是否具有此权限，否则抛出Access异常
            (IocCoreFactory.Get<IAccessDecider>() as IAccessDecider).Decide(PrincipalTokenHolder.CurrentPrincipal, pinfo);
            return pinfo;
        }

        public static readonly EmptyPermissionInfo EMPTY_PERMISSIONINFO = new EmptyPermissionInfo();
    }

    /// <summary>
    /// 此选举委托用于对权限集是否包含指定的权限进行投票
    /// </summary>
    public delegate bool Elect(PermissionInfoCollection source);

    /// <summary>
    /// 权限信息的集合，默认情况下，只要集合中有一个权限包含指定权限则包含。这种情况适合只有正向权限的情形，
    /// 如果系统需要处理负向权限，子类必须添加选举委托，以便进行选举策略决定权限集是否包含权限
    /// </summary>
    public class PermissionInfoCollection : ICollection<PermissionInfo>
    {
        protected IList<PermissionInfo> permissions = new List<PermissionInfo>();

        private Elect electVisitor;

        public Elect ElectVisitor
        {
            get { return electVisitor; }
            set { electVisitor = value; }
        }

        public PermissionInfo this[int index]
        {
            get
            {
                return permissions[index];
            }
        }

        public PermissionInfoCollection()
            : base()
        {
        }

        /// <summary>
        /// 判断列表中是否存在(单纯比较内容）
        /// </summary>
        public virtual bool Exist(PermissionInfo p)
        {
            PermissionInfo pi = permissions.First(per =>
            {
                if (per.Action.Equals(p.Action))
                    return true;
                return false;
            });
            return pi != null;
        }

        public virtual void Add(PermissionInfo item)
        {

            if (!permissions.Contains(item))
            {
                permissions.Add(item);
            }
        }

        public virtual void Clear()
        {
            permissions.Clear();
        }

        private bool DefaultContains(PermissionInfo item)
        {
            foreach (PermissionInfo per in permissions)
            {
                if (per.Contains(item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 可以使用交集并集扩展方法进行优化算法
        /// </summary>
        public virtual bool Contains(PermissionInfo item)
        {
            if (ElectVisitor != null)
                return ElectVisitor(this);
            return DefaultContains(item);
        }

        public virtual void CopyTo(PermissionInfo[] array, int arrayIndex)
        {
            permissions.CopyTo(array, arrayIndex);
        }

        public virtual int Count
        {
            get { return permissions.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return permissions.IsReadOnly; }
        }

        public virtual bool Remove(PermissionInfo item)
        {
            return permissions.Remove(item);
        }

        public virtual IEnumerator<PermissionInfo> GetEnumerator()
        {
            return permissions.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return permissions.GetEnumerator();
        }

        /// <summary>
        /// 深度克隆
        /// </summary>
        public object Clone()
        {
            PermissionInfoCollection clone = new PermissionInfoCollection();
            foreach (PermissionInfo per in permissions)
            {
                clone.Add((PermissionInfo)per.Clone());
            }
            return clone;
        }

        public override int GetHashCode()
        {
            int code = 1;
            foreach (PermissionInfo per in permissions)
            {
                code ^= per.GetHashCode();
            }
            return code;
        }

        /// <summary>
        /// 性能较低。通常权限集不进行相等比较
        /// </summary>
        public override bool Equals(object other)
        {
            if (other == this)
                return true;
            PermissionInfoCollection c = other as PermissionInfoCollection;
            if (c == null)
                return false;
            if (this.Count != c.Count)
                return false;
            for (int i = 0; i < this.Count; i++)
            {
                if (!EqualsInOther(this[i], c))
                    return false;
            }
            if (electVisitor != null)
                return electVisitor.Equals(c.electVisitor);
            else
                return c.electVisitor == null;
        }

        private bool EqualsInOther(PermissionInfo per, PermissionInfoCollection collection)
        {
            foreach (PermissionInfo p in collection)
            {
                if (p.Equals(per))
                {
                    return true;
                }
            }
            return false;
        }

        public static readonly EmptyPermissionInfoCollection EMPTY_PERMISSIONINFO_COLLECTION = new EmptyPermissionInfoCollection();
    }

    public class EmptyPermissionInfoCollection : PermissionInfoCollection
    {
        public EmptyPermissionInfoCollection()
            : base()
        {
        }

        /// <summary>
        /// 判断列表中是否存在(单纯比较内容）
        /// </summary>
        public override bool Exist(PermissionInfo p)
        {
            return false;
        }

        public override void Add(PermissionInfo item)
        {
        }

        public override void Clear()
        {
        }

        /// <summary>
        /// 可以使用交集并集扩展方法进行优化算法
        /// </summary>
        public override bool Contains(PermissionInfo item)
        {
            return false;
        }

        public override void CopyTo(PermissionInfo[] array, int arrayIndex)
        {
        }

        public override int Count
        {
            get { return 0; }
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override bool Remove(PermissionInfo item)
        {
            return false;
        }

        public override IEnumerator<PermissionInfo> GetEnumerator()
        {
            return null;
        }
    }
}
