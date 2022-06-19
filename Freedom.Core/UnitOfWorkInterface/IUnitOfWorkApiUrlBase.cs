using Freedom.Core.UnitOfWorkBase;
using Freedom.Frontend.Models.BindableSqlite;

namespace Freedom.Core.UnitOfWorkInterface
{
    public interface IUnitOfWorkApiUrlBase : IUowAddOrUpdate<ApiUrlBaseBind>,
                                             IUowGetById<ApiUrlBaseBind>,
                                             IUowGetAll<ApiUrlBaseBind>,
                                             IUowExist<ApiUrlBaseBind>,
                                             IUowUpdate<ApiUrlBaseBind>,
                                             IUowInsert<ApiUrlBaseBind>
    {
    }
}