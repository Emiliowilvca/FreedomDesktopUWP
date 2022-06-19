using Freedom.UICore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Freedom.UICore.BaseClass
{
    public abstract class BaseNavigation : IBaseNavigation
    {
        private readonly Assembly[] _assemblies;
        private List<Type> TypesCollection;
        private Frame _contentFrame;
        private SuppressNavigationTransitionInfo _transitionInfo;

        public Action<PageTitle, bool> OnNavigateAction { get; set; }

        public BaseNavigation(Assembly[] assemblies)
        {
            _assemblies = assemblies;
            TypesCollection = new List<Type>();
            _transitionInfo = new SuppressNavigationTransitionInfo();
        }

        public void InitializeFrame(Frame frame)
        {
            _contentFrame = frame;
            _contentFrame.Navigated += (s, e) =>
            {
                var vmInstance = GetViewModel<BaseViewModel>(_contentFrame.Content);
                if (vmInstance == null)
                    return;
                OnNavigateAction?.Invoke(vmInstance.PageTitle, _contentFrame.CanGoBack);
                vmInstance.NavigateAction?.Invoke(e.Parameter);
            };
        }

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

        #region Navigations

        public bool TryGoBack()
        {
            if (!_contentFrame.CanGoBack)
            {
                return false;
            }
            _contentFrame.GoBack();
            return true;
        }

        public void TryGoBack(NavigationTransitionInfo navigationTransitionInfo)
        {
            if (!_contentFrame.CanGoBack)
            {
                return;
            }
            _contentFrame.GoBack(navigationTransitionInfo);
        }

        public void NavigateTo(string pageKey, string parameter)
        {
            Type page = CreateType(pageKey);

            var currentNavPage = _contentFrame.CurrentSourcePageType;
            if (!(page is null) && !Type.Equals(page, currentNavPage))
            {
                _contentFrame.Navigate(page, parameter, _transitionInfo);
            }
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            Type page = CreateType(pageKey);

            var currentNavPage = _contentFrame.CurrentSourcePageType;
            if (!(page is null) && !Type.Equals(page, currentNavPage))
            {
                _contentFrame.Navigate(page, parameter, _transitionInfo);
            }
        }

        public void NavigateTo(string pageKey, byte[] parameter)
        {
            Type page = CreateType(pageKey);

            var currentNavPage = _contentFrame.CurrentSourcePageType;
            if (!(page is null) && !Type.Equals(page, currentNavPage))
            {
                _contentFrame.Navigate(page, parameter, _transitionInfo);
            }
        }

        public void NavigateTo(string pageKey, string parameter, NavigationTransitionInfo navigationTransitionInfo)
        {
            Type page = CreateType(pageKey);
            if (_contentFrame == null)
                return;
            var currentNavPage = _contentFrame.CurrentSourcePageType;

            if (!(page is null) && !Type.Equals(page, currentNavPage))
            {
                _contentFrame.Navigate(page, parameter, navigationTransitionInfo);
            }
        }

        public void NavigateTo(string pageKey, object parameter, NavigationTransitionInfo navigationTransitionInfo)
        {
            Type page = CreateType(pageKey);
            if (_contentFrame == null)
                return;
            var currentNavPage = _contentFrame.CurrentSourcePageType;

            if (!(page is null) && !Type.Equals(page, currentNavPage))
            {
                _contentFrame.Navigate(page, parameter, navigationTransitionInfo);
            }
        }

        public bool NavigateTo<T>(object parameter = null) where T : Page
        {
            Type page = typeof(T);

            if (_contentFrame == null)
                return false;
            var currentNavPage = _contentFrame.CurrentSourcePageType;

            if (!(page is null) && !Type.Equals(page, currentNavPage))
            {
                _contentFrame.Navigate(page, parameter, _transitionInfo);
            }

            return true;
        }

        public void RemoveFromBackStack()
        {
            _contentFrame.BackStack.Remove(_contentFrame.BackStack.Last());
        }

        #endregion Navigations

        #region Methods

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

        private Type CreateType(string pageKey)
        {
            if (TypesCollection.Count < 1)
            {
                TypesCollection = _assemblies.Where(a => !a.IsDynamic)
                                             .SelectMany(s => s.GetTypes())
                                             .ToList();
            }
            Type xType = TypesCollection.FirstOrDefault(x => x.Name.Equals(pageKey));
            return xType;
        }

        #endregion Methods
    }
}