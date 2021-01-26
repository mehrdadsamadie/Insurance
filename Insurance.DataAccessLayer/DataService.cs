using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Insurance.DataAccessLayer
{
    public class DataService<T> : IDataService<T> where T : class
    {
        private readonly IDataRepository<T> dataRepository;
        public DataService(IDataRepository<T> dataRepository)
        {
            this.dataRepository = dataRepository;
        }
        public T CreateWithSaveChange(T entity)
        {
            entity = this.dataRepository.Create(entity);
            this.dataRepository.SaveChanges();
            return entity;
        }

        public virtual void  DeleteWithSaveChange(T entity)
        {
            this.dataRepository.Delete(entity);
            this.dataRepository.SaveChanges();
        }

        public IEnumerable<T> FindAll()
        {
            return dataRepository.FindAll();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.dataRepository.FindByCondition(expression);
        }

        public void UpdateWithSaveChange(T entity)
        {
            this.dataRepository.Update(entity);
            this.dataRepository.SaveChanges();

        }
    }
}
