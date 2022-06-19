using Freedom.Core.UnitOfWorkBase;
using Freedom.Frontend.Models.SqliteModels;

namespace Freedom.Core.UnitOfWorkInterface
{
    public interface IUnitOfWorkLoginStore : IUowAddOrUpdate<LoginStore>,
                                             IUowUpdate<LoginStore>,
                                             IUowGetAll<LoginStore>,
                                             IUowInsert<LoginStore>
    {
    }
}