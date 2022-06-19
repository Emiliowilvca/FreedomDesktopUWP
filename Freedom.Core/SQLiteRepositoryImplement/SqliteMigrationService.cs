using Freedom.Core.Models;
using Freedom.Core.SQLiteConnectionFactory;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Frontend.Models.SqliteModels;

namespace Freedom.Core.SQLiteRepositoryImplement
{
    public class SqliteMigrationService : ISqliteMigrationService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SqliteMigrationService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void CreateAndMigrateAllTables()
        {
            _connectionFactory.SQLiteConnection().CreateTable<ApiUrlBase>();
            _connectionFactory.SQLiteConnection().CreateTable<DarkTheme>();
            _connectionFactory.SQLiteConnection().CreateTable<LoginHistory>();
            _connectionFactory.SQLiteConnection().CreateTable<LoginStore>();
            _connectionFactory.SQLiteConnection().CreateTable<Product>();
        }
    }
}