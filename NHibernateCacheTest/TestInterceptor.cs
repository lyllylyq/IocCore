using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Proxy.DynamicProxy;
using System.ComponentModel;
using NHibernate.Type;
using NHibernate.SqlCommand;
using System.Collections;


namespace NHibernateCacheTest
{
    public class TestInterceptor : EmptyInterceptor
    {
        int count = 1;
        public override bool OnLoad(object entity,
                                object id,
                object[] state,
                string[] propertyNames,
                IType[] types)
        {
            return base.OnLoad(entity, id, state, propertyNames, types);
        }
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            Console.WriteLine("OnPrepareStatement");
            return base.OnPrepareStatement(sql);
        }

        public override void AfterTransactionBegin(ITransaction tx)
        {
            Console.WriteLine("AfterTransactionBegin");
        }
        public override void AfterTransactionCompletion(ITransaction tx)
        {
            Console.WriteLine("AfterTransactionCompletion");
        }
        public override void BeforeTransactionCompletion(ITransaction tx)
        {
            Console.WriteLine("BeforeTransactionCompletion");
        }
        public override int[] FindDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types)
        {
            Console.WriteLine("FindDirty");
            return base.FindDirty(entity, id, currentState, previousState, propertyNames, types);
        }
        public override object GetEntity(string entityName, object id)
        {
            //Console.WriteLine("GetEntity");
            return base.GetEntity(entityName, id);
        }
        public override string GetEntityName(object entity)
        {
            Console.WriteLine("GetEntityName");
            return base.GetEntityName(entity);
        }
        public override object Instantiate(string clazz, EntityMode entityMode, object id)
        {
            //Console.WriteLine("Instantiate");
            return base.Instantiate(clazz, entityMode, id);
        }
        public override bool? IsTransient(object entity)
        {
            Console.WriteLine("IsTransient");
            return base.IsTransient(entity);
        }
        public override void OnCollectionRecreate(object collection, object key)
        {
            Console.WriteLine("OnCollectionRecreate");
        }
        public override void OnCollectionRemove(object collection, object key)
        {
            Console.WriteLine("OnCollectionRemov");
        }
        public override void OnCollectionUpdate(object collection, object key)
        {
            Console.WriteLine("OnCollectionUpdate");
        }
        public override void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            Console.WriteLine("OnDelete");
        }
        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types)
        {
            Console.WriteLine("OnFlushDirty");
            return OnFlushDirty(entity, id, currentState, previousState, propertyNames, types);
        }
        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            Console.WriteLine("OnSave");
            return base.OnSave(entity, id, state,propertyNames, types);
        }
        public override void PostFlush(ICollection entities)
        {
            Console.WriteLine("PostFlush");
        }
        public override void PreFlush(ICollection entitites)
        {
            Console.WriteLine("PreFlush");
        }
        public override void SetSession(ISession session)
        {
            Console.WriteLine("SetSession");
        }
    }
}
