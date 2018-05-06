using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using SQLite;
using Xamarin.Forms;

namespace MeltingApp.Services
{
    public class DataBaseService : IDataBaseService
    {
        private readonly object _lockObject = new object();
        public SQLiteConnection SQLiteConnection { get; set; }

        public DataBaseService()
        {
            var fileLocatorService = DependencyService.Get<IFileLocatorService>();
            SQLiteConnection = new SQLiteConnection(fileLocatorService.GetDataBasePath());
        }

        public void DropTables()
        {
            if (!SQLiteConnection.IsInTransaction)
            {
                SQLiteConnection.BeginTransaction();
            }
            SQLiteConnection.DropTable<Token>();
            SQLiteConnection.DropTable<User>();
            SQLiteConnection.Commit();
        }

        public void InitializeTables()
        {
            if(!SQLiteConnection.IsInTransaction)
            {
                SQLiteConnection.BeginTransaction();
            }
            SQLiteConnection.CreateTable<User>();
            SQLiteConnection.CreateTable<Token>();
            SQLiteConnection.Commit();
        }

        public T GetWithChildren<T>(Expression<Func<T, bool>> predicate) where T : EntityBase, new()
        {
            throw new NotImplementedException();
        }

        public List<T> GetCollection<T>(Expression<Func<T, bool>> predicate = null) where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                if (predicate == null) return SQLiteConnection.Table<T>().Where(arg => true).ToList();
                return SQLiteConnection.Table<T>().Where(predicate).ToList();
            }
        }

        public List<T> GetCollectionWithChildren<T>(Expression<Func<T, bool>> predicate = null) where T : EntityBase, new()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                if (predicate == null) return null;
                return SQLiteConnection.Table<T>().FirstOrDefault(predicate);
            }

        }


        public int Insert<T>(T entity) where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }

                var id = SQLiteConnection.Insert(entity);
                SQLiteConnection.Commit();
                return id;
            }
        }

        public int InsertWithChildren<T>(T entity) where T : EntityBase, new()
        {
            throw new NotImplementedException();
        }

        public void UpdateCollectionWithChildren<T>(T entities) where T : IEnumerable
        {
            throw new NotImplementedException();
        }

        public void Clear<T>() where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }

                SQLiteConnection.DeleteAll<T>();
                SQLiteConnection.Commit();
            }
        }

        public void InsertCollection<T>(T entities) where T : IEnumerable
        {
            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }
                SQLiteConnection.InsertAll(entities);
                SQLiteConnection.Commit();
            }
        }

        public void InsertCollectionWithChildren<T>(T entities) where T : IEnumerable
        {
            throw new NotImplementedException();
        }

        public int Update<T>(T entity) where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }
                int id;
                id = Get<T>(t => t.dbId == entity.dbId) == null ? Insert(entity) : SQLiteConnection.Update(entity);
                SQLiteConnection.Commit();
                return id;
            }
        }

        public int UpdateWithChildren<T>(T entity) where T : EntityBase, new()
        {
            throw new NotImplementedException();
        }

        public void UpdateCollection<T>(T entities) where T : IEnumerable
        {
            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }
                SQLiteConnection.UpdateAll(entities);
                SQLiteConnection.Commit();
            }
        }

        public List<T> FindWithPagination<T>(int skip, int take, Expression<Func<T, bool>> predicate = null) where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                if (predicate == null)
                    return SQLiteConnection.Table<T>().Where(t => true).Skip(skip).Take(take).ToList();
                return SQLiteConnection.Table<T>().Where(predicate).Skip(skip).Take(take).ToList();
            }
        }
    }
}
