﻿using Core;
using DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public abstract class ManagerBase<T>:IDataAccess<T> where T : class
    {
        private Repository<T> repo = new Repository<T>();

        public virtual List<T> List()
        {
            return repo.List();
        }

        public virtual List<T> List(Expression<Func<T, bool>> where)
        {
            return repo.List(where);
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public virtual int Insert(T obj)
        {
            return repo.Insert(obj);
        }

        public virtual int Update(T obj)
        {
            return repo.Update(obj);
        }
        public virtual int Delete(T obj)
        {
            return repo.Delete(obj);
        }

        public virtual int Save()
        {
            return repo.Save();
        }

        public virtual T Find(Expression<Func<T, bool>> where)
        {
            return repo.Find(where);
        }
    }
}
