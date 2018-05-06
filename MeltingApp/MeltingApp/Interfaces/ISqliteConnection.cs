using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net;

namespace MeltingApp.Interfaces
{
    public interface ISqliteConnection
    {
        SQLiteConnection SqLiteConnection { get; set; }
    }
}
