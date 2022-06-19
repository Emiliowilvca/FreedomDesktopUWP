using Freedom.Core.SQLiteConnectionFactory;
using Freedom.Core.SQLiteGenericRepository;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Frontend.Models.SqliteModels;
using System.Collections.Generic;
using System.Linq;

namespace Freedom.Core.SQLiteRepositoryImplement
{
    public class LoginHistoryRepository : GenericRepository<LoginHistory>, ILoginHistoryRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public LoginHistoryRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<string> GetUserEmailsDistinct()
        {
            return _connectionFactory.SQLiteConnection()
                                     .Table<LoginHistory>()
                                     .Select(x => x.Email)
                                     .Distinct()
                                     .ToList();
        }
    }
}