using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Freedom.UICore.Implement
{
    public class ShellNavigationService : IShellNavigationService
    {
        private Frame _shellFrame;
        private object _lastParameterUsed;
        private readonly Assembly[] _assemblies;
        private List<Type> TypesCollection;

        public ShellNavigationService(Assembly[] assemblies)
        {
            _assemblies = assemblies;
            TypesCollection = new List<Type>();
        }

        public void Initialize(Frame shellFrame)
        {
            if (_shellFrame == null)
            {
                _shellFrame = shellFrame;
                _shellFrame.Navigated += (s, e) =>
                {
                    var vmInstance = GetViewModel<BaseViewModel>(_shellFrame.Content);
                    if (vmInstance == null)
                        return;
                    vmInstance.NavigateAction?.Invoke(e.Parameter);
                };
            }
        }

        public bool IsShell()
        {
            if (_shellFrame.CurrentSourcePageType == null || _shellFrame.CurrentSourcePageType.Name.EndsWith("ShellView"))
                return true;
            else
                return false;
        }

        public bool CanGoBack() => _shellFrame.CanGoBack;

        public bool CanGoForward() => _shellFrame.CanGoForward;

        public bool GoBack()
        {
            if (CanGoBack())
            {
                _shellFrame.GoBack();
                return true;
            }

            return false;
        }

        public void GoForward() => _shellFrame.GoForward();

        public void RemoveFromBackStack()
        {
            _shellFrame.BackStack.Remove(_shellFrame.BackStack.Last());
        }

        public bool Navigate(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            if (_shellFrame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed)))
            {
                var navigationResult = _shellFrame.Navigate(pageType, parameter, infoOverride);
                if (navigationResult)
                {
                    _lastParameterUsed = parameter;
                }

                return navigationResult;
            }
            else
            {
                return false;
            }
        }

        public bool Navigate<T>(object parameter = null, NavigationTransitionInfo infoOverride = null) where T : Page
        {
            return Navigate(typeof(T), parameter, infoOverride);
        }

        #region Private

        private T GetViewModel<T>(object content) where T : class
        {
            var pageKey = content.GetType().Name;
            string vmName = $"{pageKey.Replace("Page", "")}ViewModel";
            Type VMtype = GetTypeByClassName(vmName);

            if (VMtype == null)
                return null;
            return content.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                          .Where(x => x.PropertyType == VMtype)
                          .Select(s => (T)s.GetValue(content))
                          .FirstOrDefault();
        }

        private Type GetTypeByClassName(string className)
        {
            if (TypesCollection.Count < 1)
            {
                TypesCollection = _assemblies.Where(a => !a.IsDynamic)
                                             .SelectMany(s => s.GetTypes())
                                             .ToList();
            }
            Type xType = TypesCollection.FirstOrDefault(x => x.Name.Equals(className));
            return xType;
        }

        #endregion Private
    }
}