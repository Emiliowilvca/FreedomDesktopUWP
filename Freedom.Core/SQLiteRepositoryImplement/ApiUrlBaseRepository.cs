using Freedom.Core.SQLiteConnectionFactory;
using Freedom.Core.SQLiteGenericRepository;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Frontend.Models.SqliteModels;

namespace Freedom.Core.SQLiteRepositoryImplement
{
    public class ApiUrlBaseRepository : GenericRepository<ApiUrlBase>, IApiUrlBaseRepository
    {
        public ApiUrlBaseRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}