using Freedom.Core.UnitOfWorkBase;
using Freedom.Frontend.Models.SqliteModels;

namespace Freedom.Core.UnitOfWorkInterface
{
    public interface IUnitOfWorkDarkTheme : IUowAddOrUpdate<DarkTheme>,
                                            IUowGetById<DarkTheme>,
                                            IUowGetAll<DarkTheme>,
                                            IUowExist<DarkTheme>,
                                            IUowUpdate<DarkTheme>,
                                            IUowInsert<DarkTheme>
    {
    }
}