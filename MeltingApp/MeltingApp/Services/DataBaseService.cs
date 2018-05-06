using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MeltingApp.Interfaces;
using MeltingApp.Models;
using SQLite.Net;
using Xamarin.Forms;

namespace MeltingApp.Services
{
    public class DataBaseService : IDataBaseService
    {
        private readonly object _lockObject = new object();
        private SQLiteConnection connection => SqLiteConnection.SqLiteConnection;
        public static ISqliteConnection SqLiteConnection { get; set; }

        public DataBaseService()
        {
            SqLiteConnection = DependencyService.Get<ISqliteConnection>();
        }

        public void DropTables()
        {
            connection.DropTable<User>();
        }

        public void InitializeTables()
        {
            connection.CreateTable<User>();
        }

        public List<T> GetCollection<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            lock (_lockObject)
            {
                if (predicate == null) return connection.Table<T>().Where(arg => true).ToList();
                return connection.Table<T>().Where(predicate).ToList();
            }
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            lock (_lockObject)
            {
                if (predicate == null) return null;
                return connection.Table<T>().Where(predicate).FirstOrDefault();
            }

        }

        public void Insert<T>(T entity) where T : class
        {
            lock (_lockObject)
            {
                connection.Insert(entity);
                connection.Commit();
            }
        }

        public void Clear<T>()
        {
            lock (_lockObject)
            {
                connection.DeleteAll<T>();
                connection.Commit();
            }
        }

        public void InsertCollection<T>(T entities) where T : IEnumerable
        {
            lock (_lockObject)
            {
                connection.InsertAll(entities);
                connection.Commit();
            }
        }

        public void Update<T>(T entity) where T : class
        {
            lock (_lockObject)
            {
                connection.Update(entity);
                connection.Commit();
            }
        }

        public void UpdateCollection<T>(T entities) where T : IEnumerable
        {
            lock (_lockObject)
            {
                connection.UpdateAll(entities);
                connection.Commit();
            }
        }

        public List<T> FindWithPagination<T>(int skip, int take, Expression<Func<T, bool>> predicate = null) where T : class
        {
            lock (_lockObject)
            {
                if (predicate == null)
                    return connection.Table<T>().Where(t => true).Skip(skip).Take(take).ToList();
                return connection.Table<T>().Where(predicate).Skip(skip).Take(take).ToList();
            }
        }
    }
}
