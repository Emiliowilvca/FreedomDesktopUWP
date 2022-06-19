using Freedom.Core.SQLiteConnectionFactory;
using Freedom.Core.SQLiteGenericRepository;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Frontend.Models.SqliteModels;

namespace Freedom.Core.SQLiteRepositoryImplement
{
    public class DarkThemeRepository : GenericRepository<DarkTheme>, IDarkThemeRepository
    {
        public DarkThemeRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}