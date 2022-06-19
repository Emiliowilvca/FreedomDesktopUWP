using System;

namespace Freedom.UICore.Interface
{
    public interface IReloadService
    {
        void Subscribe(string pageName,Type modelType);

        void UnSubscribe(string pageName, Type modelType);

        void NotifyChanges(Type type);

        void ConfirmAction(string pageName, Type modelType);

        bool IsRecharge(string pageName, Type modelType, Action actionForReload, bool confirmAction);
    }
}