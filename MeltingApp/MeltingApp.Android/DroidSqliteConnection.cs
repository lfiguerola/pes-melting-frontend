using System;
using System.IO;
using MeltingApp.Interfaces;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

namespace MeltingApp.Droid
{
    public class DroidSqliteConnection : ISqliteConnection
    {
        public SQLiteConnection SqLiteConnection { get; set; }

        public DroidSqliteConnection()
        {
            SqLiteConnection = GetConnection();
        }
        private SQLiteConnection GetConnection()
        {
            const string sqliteFilename = "MeltingAppDB.db3";

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            var plat = new SQLitePlatformAndroid();
            SQLiteConnectionWithLock conn;
            try
            {
                conn = new SQLiteConnectionWithLock(plat, new SQLiteConnectionString(path, true));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return conn;
        }
    }
}