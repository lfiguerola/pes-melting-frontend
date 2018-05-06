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
        T Get<T>(Expression<Func<T, bool>> predicate) where T : class, new();
        List<T> GetCollection<T>(Expression<Func<T, bool>> predicate = null) where T : class, new();
        List<T> FindWithPagination<T>(int skip, int take, Expression<Func<T, bool>> predicate = null) where T : class, new();
        void Insert<T>(T entity) where T : class, new();
        void InsertCollection<T>(T entities) where T : IEnumerable;
        void Update<T>(T entity) where T : class, new();
        void UpdateCollection<T>(T entities) where T : IEnumerable;
        void Clear<T>() where T : class, new();
    }
}
