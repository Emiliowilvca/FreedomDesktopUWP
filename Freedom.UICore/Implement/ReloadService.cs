using Freedom.UICore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Freedom.UICore.Implement
{
    public class ReloadService : IReloadService
    {
        private List<ReloadItem> ReloadCollection { get; set; }

        public ReloadService()
        {
            ReloadCollection = new List<ReloadItem>();
        }

        public void Subscribe(string pageName, Type type)
        {
            int count = ReloadCollection.Where(x => x.PageName == pageName && x.ModelType == type).Count();
            if (count > 0) return;

            ReloadCollection.Add(new ReloadItem(pageName, type, false));
        }

        public void UnSubscribe(string pageName, Type modelType)
        {
            var item = ReloadCollection.FirstOrDefault(x => x.PageName == pageName && x.ModelType == modelType);
            if (item == null) return;
            ReloadCollection.Remove(item);
        }

        public void NotifyChanges(Type type)
        {
            ReloadCollection.Where(x => x.ModelType == type).ToList().ForEach(x => x.Recharge = true);
        }

        public void ConfirmAction(string pageName, Type modelType)
        {
            ReloadCollection.Where(x => x.ModelType == modelType && x.PageName == pageName)
                            .ToList()
                            .ForEach(x => x.Recharge = false);
        }

        public bool IsRecharge(string pageName, Type modelType, Action actionForReload, bool confirmAction)
        {
            var item = ReloadCollection.Where(x => x.ModelType == modelType && x.PageName == pageName).FirstOrDefault();

            if (item == null)
            {
                return false;
            }
            else
            {
                if (item.Recharge)
                {
                    actionForReload?.Invoke();
                    if (confirmAction)
                    {
                        ConfirmAction(pageName, modelType);
                    }
                }
                return item.Recharge;
            }
        }

        private class ReloadItem
        {
            public ReloadItem(string pageName, Type modelType, bool recharge)
            {
                PageName = pageName;
                ModelType = modelType;
                Recharge = recharge;
            }

            public string PageName { get; set; }

            public Type ModelType { get; set; }

            public bool Recharge { get; set; }
        }
    }
}