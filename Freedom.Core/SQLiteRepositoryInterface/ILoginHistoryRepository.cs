using Freedom.Core.SQLiteGenericRepository;
using Freedom.Frontend.Models.SqliteModels;
using System.Collections.Generic;

namespace Freedom.Core.SQLiteRepositoryInterface
{
    public interface ILoginHistoryRepository : IGenericRepository<LoginHistory>
    {

        List<string> GetUserEmailsDistinct();
    }
}