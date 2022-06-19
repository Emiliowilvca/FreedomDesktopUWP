using Freedom.Core.SQLiteConnectionFactory;
using Freedom.Core.SQLiteGenericRepository;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Frontend.Models.SqliteModels;
namespace Freedom.Core.SQLiteRepositoryImplement
{
    public class LoginStoreRepository : GenericRepository<LoginStore>, ILoginStoreRepository
    {
        public LoginStoreRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}