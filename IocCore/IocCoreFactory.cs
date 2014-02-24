using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
namespace IocCore
{
    public class IocCoreFactory
    {
        private static IWindsorContainer container = null;
        public static void InitContainer(string xmlpath)
        {
            container = new WindsorContainer(new XmlInterpreter(xmlpath));
        }

        public static T Get<T>(String key)
        {
            return resolve<T>(key);
        }
        public static T Get<T>()
        {
            return resolve<T>(null);
        }
        
        private static T resolve<T>(String key)
        {
            T obj = default(T);
            if (key != null)
                obj = getContainer().Resolve<T>(key);
            else
                obj = getContainer().Resolve<T>();
            System.Diagnostics.Debug.Assert(obj != null, "生成Ioc对象失败！Type:" + typeof(T));
            return obj;
        }
        private static IWindsorContainer getContainer()
        {
            if (container == null)
            {
                container = new WindsorContainer(new XmlInterpreter("ClientBin\\IocConfig.xml"));
            }
            return container;
        }
    }
}
