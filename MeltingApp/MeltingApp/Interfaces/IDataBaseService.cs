using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MeltingApp.Models;

namespace MeltingApp.Interfaces
{
    public interface IDataBaseService
    {
        void DropTables();
        void InitializeTables();
        T Get<T>(Expression<Func<T, bool>> predicate) where T : EntityBase, new();
        T GetWithChildren<T>(Expression<Func<T, bool>> predicate) where T : EntityBase, new();
        List<T> GetCollection<T>(Expression<Func<T, bool>> predicate = null) where T : EntityBase, new();
        List<T> GetCollectionWithChildren<T>(Expression<Func<T, bool>> predicate = null) where T : EntityBase, new();
        List<T> FindWithPagination<T>(int skip, int take, Expression<Func<T, bool>> predicate = null) where T : EntityBase, new();
        List<T> FindWithChildrenWithPagination<T>(int skip, int take, Expression<Func<T, bool>> predicate = null) where T : EntityBase, new();
        int Insert<T>(T entity) where T : EntityBase, new();
        int InsertWithChildren<T>(T entity) where T : EntityBase, new();
        void InsertCollection<T>(T entities) where T : IEnumerable;
        void InsertCollectionWithChildren<T>(T entities) where T : IEnumerable;
        int Update<T>(T entity) where T : EntityBase, new();
        int UpdateWithChildren<T>(T entity) where T : EntityBase, new();
        void UpdateCollection<T>(T entities) where T : IEnumerable;
        void UpdateCollectionWithChildren<T>(T entities) where T : IEnumerable;
        void Clear<T>() where T : EntityBase, new();
    }
}
