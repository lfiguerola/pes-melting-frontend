using System;
using System.IO;
using MeltingApp.Interfaces;

namespace MeltingApp.Droid
{
    public class AndroidFileLocatorService : IFileLocatorService
    {
        public string GetDataBasePath()
        {
            const string sqliteFilename = "MeltingAppDB.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);

            return path;
        }
    }
}