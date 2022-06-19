namespace Freedom.Core.SQLiteRepositoryInterface
{
    public interface ISqliteMigrationService
    {
        void CreateAndMigrateAllTables();
    }
}