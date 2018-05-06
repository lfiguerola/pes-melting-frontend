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
            //var connectionString = new SQLiteConnectionString(fileLocatorService.GetDataBasePath(), true);
            //SQLiteConnection = new SQLiteConnectionWithLock(connectionString,SQLiteOpenFlags.Create);
            SQLiteConnection = new SQLiteConnection(fileLocatorService.GetDataBasePath());
        }

        public void DropTables()
        {
            SQLiteConnection.DropTable<User>();
        }

        public void InitializeTables()
        {
            SQLiteConnection.CreateTable<User>();
            SQLiteConnection.CreateTable<Token>();
        }

        public List<T> GetCollection<T>(Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            lock (_lockObject)
            {
                if (predicate == null) return SQLiteConnection.Table<T>().Where(arg => true).ToList();
                return SQLiteConnection.Table<T>().Where(predicate).ToList();
            }
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class, new()
        {
            lock (_lockObject)
            {
                if (predicate == null) return null;
                return SQLiteConnection.Table<T>().Where(predicate).FirstOrDefault();
            }

        }


        public void Insert<T>(T entity) where T : class, new()
        {
            lock (_lockObject)
            {
                    SQLiteConnection.Insert(entity);
                    SQLiteConnection.Commit();
                
            }
        }

        public void Clear<T>() where T : class, new()
        {
            lock (_lockObject)
            {
                SQLiteConnection.DeleteAll<T>();
                SQLiteConnection.Commit();
            }
        }

        public void InsertCollection<T>(T entities) where T : IEnumerable
        {
            lock (_lockObject)
            {
                SQLiteConnection.InsertAll(entities);
                SQLiteConnection.Commit();
            }
        }

        public void Update<T>(T entity) where T : class, new()
        {
            lock (_lockObject)
            {
                SQLiteConnection.Update(entity);
                SQLiteConnection.Commit();
            }
        }

        public void UpdateCollection<T>(T entities) where T : IEnumerable
        {
            lock (_lockObject)
            {
                SQLiteConnection.UpdateAll(entities);
                SQLiteConnection.Commit();
            }
        }

        public List<T> FindWithPagination<T>(int skip, int take, Expression<Func<T, bool>> predicate = null) where T : class, new()
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
