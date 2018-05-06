using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using SQLite.Net;
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
            SQLiteConnection.CreateTable<Event>();
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
            SQLiteConnection.CreateTable<Event>();
            SQLiteConnection.Commit();
        }

        public T GetWithChildren<T>(Expression<Func<T, bool>> predicate) where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                if (predicate == null) return null;
                var entity = Get(predicate);
                if (entity == null) return default(T);
                return SQLiteConnection.GetWithChildren<T>(entity.dbId, true);
            }
        }

        public List<T> GetCollection<T>(Expression<Func<T, bool>> predicate = null) where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                if (predicate == null) return SQLiteConnection.Table<T>().Where(arg => true).ToList();
                return SQLiteConnection.Table<T>().Where(predicate).ToList();
            }
        }

        public List<T> GetCollectionWithChildren<T>(Expression<Func<T, bool>> predicate = null)
            where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                if (predicate == null) return SQLiteConnection.GetAllWithChildren<T>(p => true);
                return SQLiteConnection.GetAllWithChildren(predicate);
            }
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                if (predicate == null) return null;
                return SQLiteConnection.Table<T>().FirstOrDefault(predicate);
            }
        }

        public List<T> FindWithChildrenWithPagination<T>(int skip, int take, Expression<Func<T, bool>> predicate = null)
            where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                return GetCollectionWithChildren(predicate).Skip(skip).Take(take).ToList();
            }
        }

        public int Insert<T>(T entity) where T : EntityBase, new()
        {
            if (entity == null) return -1;
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
            if (entity == null) return -1;
            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }

                SQLiteConnection.InsertWithChildren(entity, true);
                SQLiteConnection.Commit();
                return entity.dbId;
            }
        }

        public void UpdateCollectionWithChildren<T>(T entities) where T : IEnumerable
        {
            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }

                SQLiteConnection.UpdateWithChildren(entities);
                SQLiteConnection.Commit();
            }
        }

        public void Clear<T>() where T : EntityBase, new()
        {
            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }
                WriteOperations.DeleteAll(SQLiteConnection, GetCollection<T>(t => true), recursive: true);
                SQLiteConnection.Commit();
            }
        }

        public void InsertCollection<T>(T entities) where T : IEnumerable
        {
            if (entities == null) return;
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
            if (entities == null) return;
            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }

                SQLiteConnection.InsertAllWithChildren(entities, true);
                SQLiteConnection.Commit();
            }
        }

        public int Update<T>(T entity) where T : EntityBase, new()
        {
            if (entity == null) return -1;

            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }
                int id;
                if (Get<T>(t => t.dbId == entity.dbId) == null)
                {
                    id = SQLiteConnection.Insert(entity);
                }
                else
                {
                    id = SQLiteConnection.Update(entity);
                }
                SQLiteConnection.Commit();
                return id;
            }
        }

        public int UpdateWithChildren<T>(T entity) where T : EntityBase, new()
        {
            if (entity == null) return -1;

            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }

                if (GetWithChildren<T>(t => t.dbId == entity.dbId) == null)
                {
                    SQLiteConnection.InsertWithChildren(entity, true);
                }
                else
                {
                    SQLiteConnection.UpdateWithChildren(entity);
                }

                SQLiteConnection.Commit();
                return entity.dbId;
            }
        }

        public void UpdateCollection<T>(T entities) where T : IEnumerable
        {
            if (entities == null) return;

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

        public void Delete<T>(T entity, bool recursive = true) where T : EntityBase, new()
        {
            if (entity == null) return;
            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }
                WriteOperations.Delete(SQLiteConnection, entity, recursive);
                SQLiteConnection.Commit();
            }
        }

        public void DeleteCollection<T>(T entities, bool recursive = true) where T : IEnumerable
        {
            if (entities == null) return;
            lock (_lockObject)
            {
                if (!SQLiteConnection.IsInTransaction)
                {
                    SQLiteConnection.BeginTransaction();
                }
                WriteOperations.DeleteAll(SQLiteConnection, entities, recursive);
                SQLiteConnection.Commit();
            }
        }

    }
}
