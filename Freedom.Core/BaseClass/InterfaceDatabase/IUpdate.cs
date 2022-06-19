using Freedom.Utility.DatabaseResponse;

namespace Freedom.Core.BaseClass.InterfaceDatabase
{
    public interface IUpdate<T> where T : class
    {
        DbResponse Update(T entity);
    }
}