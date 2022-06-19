using Freedom.Utility.DatabaseResponse;

namespace Freedom.Core.BaseClass.InterfaceDatabase
{
    public interface IAddOrUpdate<T>
    {
        DbResponse AddOrUpdate(T entity);
    }
}