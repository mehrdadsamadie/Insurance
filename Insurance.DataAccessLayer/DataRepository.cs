using Insurance.DataAccessLayer;
using Insurance.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Insurance.DataAccessLayer
{
    public abstract class DataRepository<T> : IDataRepository<T> where T : class
    {
        protected InsuranceContext insuranceContext { get; set; }
        public DataRepository(InsuranceContext insuranceContext) {
            this.insuranceContext = insuranceContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.insuranceContext.Set<T>().AsNoTracking();
        }

        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.insuranceContext.Set<T>().Where(expression).AsNoTracking();
        }

        public virtual T Create(T entity)
        {
            this.insuranceContext.Set<T>().Add(entity);
            return entity;
        }

        public void Update(T entity)
        {
            this.insuranceContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.insuranceContext.Set<T>().Remove(entity);
        }
    }
}
