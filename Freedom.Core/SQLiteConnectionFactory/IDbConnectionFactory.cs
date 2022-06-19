using SQLite;

namespace Freedom.Core.SQLiteConnectionFactory
{
    public interface IDbConnectionFactory
    {
        string Connectionstring { get; }

        SQLiteAsyncConnection SQLiteAsyncConnection();

        SQLiteConnection SQLiteConnection();
    }
}