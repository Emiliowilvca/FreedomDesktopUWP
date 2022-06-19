using Freedom.Utility.DatabaseResponse;

namespace Freedom.Core.BaseClass.InterfaceDatabase
{
    public interface IAdd<T>
    {
        DbResponse Insert(T entity);
    }
}