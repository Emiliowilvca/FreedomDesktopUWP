using System;

namespace Freedom.UICore.Interface
{
    public interface IFindResourcesService
    {
        T Get<T>(string resourceName) where T : class;

        T GetStruct<T>(string resourceName) where T : IConvertible;

        int GetBusyDelay();

        int GetCancelTokenAfter();

        int GetCancelTokenAfter(int repeat);
    }
}