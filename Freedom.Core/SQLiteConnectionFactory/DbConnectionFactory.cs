using SQLite;
using System.IO;

namespace Freedom.Core.SQLiteConnectionFactory
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        //C:\Users\Emilio Wildberger\AppData\Local\Freedom
        public const string DatabaseFilename = "FreedomSQLite.db3";

        public const SQLiteOpenFlags Flags =
                        // open the database in read/write mode
                        SQLiteOpenFlags.ReadWrite |
                        // create the database if it doesn't exist
                        SQLiteOpenFlags.Create |
                        // enable multi-threaded database access
                        SQLiteOpenFlags.SharedCache;

        public DbConnectionFactory(string dbPath)
        {
            string fileDirectory = Path.Combine(dbPath, "Freedom");

            if (!File.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            Connectionstring = Path.Combine(fileDirectory, DatabaseFilename);
        }

        public string Connectionstring { get; private set; }

        public SQLiteAsyncConnection SQLiteAsyncConnection()
        {
            var connection = new SQLiteAsyncConnection(Connectionstring, Flags);

            return connection;
        }

        public SQLiteConnection SQLiteConnection()
        {
            var connection = new SQLiteConnection(Connectionstring, Flags);
            return connection;
        }
    }
}