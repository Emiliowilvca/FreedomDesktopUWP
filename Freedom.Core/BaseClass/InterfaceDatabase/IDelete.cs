using Freedom.Utility.DatabaseResponse;

namespace Freedom.Core.BaseClass.InterfaceDatabase
{
    public interface IDelete<T> where T : class
    {
        DbResponse Delete(T entity);
    }
}