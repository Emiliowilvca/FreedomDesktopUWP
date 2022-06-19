using Freedom.Core.Models;
using Freedom.Core.SQLiteConnectionFactory;
using Freedom.Core.SQLiteGenericRepository;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Frontend.Models.SqliteModels;
namespace Freedom.Core.SQLiteRepositoryImplement
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}