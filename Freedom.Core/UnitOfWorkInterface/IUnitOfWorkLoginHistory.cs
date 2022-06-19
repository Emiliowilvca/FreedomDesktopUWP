using Freedom.Core.UnitOfWorkBase;
using Freedom.Frontend.Models.BindableSqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freedom.Core.UnitOfWorkInterface
{
    public interface IUnitOfWorkLoginHistory : IUowGetAll<LoginHistoryBind>,
                                                IUowInsert<LoginHistoryBind>
    {
        Task<List<string>> GetUserNames();
    }
}