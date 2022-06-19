using Freedom.UICore.Interface;
using System;
using Windows.UI.Xaml;

namespace Freedom.UICore.Implement
{
    public class FindResourceService : IFindResourcesService
    {
        public T Get<T>(string resourceName) where T : class
        {
            return Application.Current.Resources[resourceName] as T;
        }

        public T GetStruct<T>(string resourceName) where T : IConvertible
        {
            return (T)Application.Current.Resources[resourceName];
        }

        public int GetBusyDelay()
        {
            try
            {
                return Convert.ToInt32(Application.Current.Resources["IsBusyDelayTime"]);
            }
            catch (Exception ex)
            {
                throw new Exception($"On Get IsBusyDelay{ex.Message}");
            }
        }

        public int GetCancelTokenAfter()
        {
            try
            {
                return Convert.ToInt32(Application.Current.Resources["CancelTokenAfter"]);
            }
            catch (Exception ex)
            {
                throw new Exception($"On Get GetCancelTokenAfter{ex.Message}");
            }
        }

        public int GetCancelTokenAfter(int repeat)
        {
            try
            {
                int tokenDelay = Convert.ToInt32(Application.Current.Resources["CancelTokenAfter"]);

                return tokenDelay * repeat;
            }
            catch (Exception ex)
            {
                throw new Exception($"On Get GetCancelTokenAfter{ex.Message}");
            }
        }
    }
}