using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Core.InterceptorHandler;
using SmartBox.Console.Dao;
using System.Reflection;
using Beyondbit.Framework.Sor.Attributes;

namespace SmartBox.Console.Bo
{
    public class BaseBO<T> where T : class, new()
    {
        protected BaseDao<T> _dao = null;
        public BaseBO(BaseDao<T> dao)
        {
            _dao = dao;
        }
        
        public BaseBO()
        {
            
        }

        public BaseBO(string configName)
        {
            _dao = new CommonDao<T>(configName);
        }

        [Frame(false, false)]
        public virtual T Get(object pkid)
        {
            return _dao.Get(pkid);
        }

        [Frame(false, false)]
        public virtual T Get(IList<KeyValuePair<string, object>> pars)
        {
            return _dao.Get(pars);
        }

        [Frame(false, false)]
        public virtual List<T> QueryList(IList<Tuple<string, string, object>> pars)
        {
            return _dao.QueryList(pars);
        }

        private object getPKValue(T entity)
        {
            Type t = typeof(T);
            System.Reflection.MemberInfo info = typeof(T);
            PropertyInfo[] properties = t.GetProperties();
            object pk = null;
            foreach (PropertyInfo p in properties)
            {
                ColumnAttribute[] attrs = (ColumnAttribute[])p.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    foreach (ColumnAttribute attr in attrs)
                    {
                        if (attr.ColumnType == ColumnType.IdentityAndPrimaryKey || attr.ColumnType == ColumnType.PrimaryKey || attr.ColumnType == ColumnType.Identity)
                        {
                            pk = p.GetValue(entity, null);
                            break;
                        }
                    }
                }
            }
            return pk;
        }

        [Frame(false, false)]
        public virtual int Save(T entity)
        {
            object pk = getPKValue(entity);
            if (_dao.Get(pk) == null)
                return _dao.Insert(entity);
            else
                return _dao.Update(entity);
        }

        [Frame(false, false)]
        public virtual int Delete(T obj)
        {
            return _dao.Delete(obj);
        }
        
        [Frame(false, false)]
        public virtual void DeleteList(IList<T> objs)
        {
            _dao.DeleteList(objs);
        }
        
        [Frame(false, false)]
        public virtual bool ExistMaxId()
        {
            return _dao.ExistMaxId();
        }
        
        [Frame(false, false)]
        public virtual bool ExistMaxId(string tableName)
        {
            return _dao.ExistMaxId(tableName);
        }
        
        [Frame(false, false)]
        public virtual T GetByObject(Object o)
        {
            return _dao.GetByObject(o);
        }
        
        [Frame(false, false)]
        public virtual int GetMaxId(string tableName)
        {
            return _dao.GetMaxId(tableName);
        }
        
        [Frame(false, false)]
        public virtual int GetMaxId()
        {
            return _dao.GetMaxId();
        }
        
        [Frame(false, false)]
        public virtual int Insert(T obj)
        {
            return _dao.Insert(obj);
        }
        
        [Frame(false, false)]
        public virtual void InsertBatch(IList<T> lst, int batchSize)
        {
            _dao.InsertBatch(lst, batchSize);
        }
        
        [Frame(false, false)]
        public virtual void InsertBatch(IList<T> lst)
        {
            _dao.InsertBatch(lst);
        }
        
        [Frame(false, false)]
        public virtual int InsertList(IList<T> lst)
        {
            return _dao.InsertList(lst);
        }
        
        [Frame(false, false)]
        public virtual int Update(T obj)
        {
            return _dao.Update(obj);
        }
        
        [Frame(false, false)]
        public virtual int UpdateList(IList<T> lst)
        {
            return _dao.UpdateList(lst);
        }
        
        [Frame(false, false)]
        public virtual void UpdateMaxId(string tableName)
        {
            _dao.UpdateMaxId(tableName);
        }
        
        [Frame(false, false)]
        public virtual void UpdateMaxId()
        {
            _dao.UpdateMaxId();
        }
    }
}
