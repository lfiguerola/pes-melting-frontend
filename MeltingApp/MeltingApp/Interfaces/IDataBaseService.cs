using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MeltingApp.Interfaces
{
    public interface IDataBaseService
    {
        void DropTables();
        void InitializeTables();
        T Get<T>(Expression<Func<T, bool>> predicate) where T : class;
        List<T> GetCollection<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        List<T> FindWithPagination<T>(int skip, int take, Expression<Func<T, bool>> predicate = null) where T : class;
        void Insert<T>(T entity) where T : class;
        void InsertCollection<T>(T entities) where T : IEnumerable;
        void Update<T>(T entity) where T : class;
        void UpdateCollection<T>(T entities) where T : IEnumerable;
        void Clear<T>();
    }
}
