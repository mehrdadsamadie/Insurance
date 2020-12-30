using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Insurance.DataAccessLayer
{
    public interface IDataService<T>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T CreateWithSaveChange(T entity);
        void UpdateWithSaveChange(T entity);
        void DeleteWithSaveChange(T entity);
    }
}
