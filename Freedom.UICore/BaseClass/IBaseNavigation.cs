using Freedom.UICore.Models;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Freedom.UICore.BaseClass
{
    public interface IBaseNavigation
    {
        Action<PageTitle, bool> OnNavigateAction { get; set; }

        void NavigateTo(string pageKey, string parameter);

        void NavigateTo(string pageKey, object parameter);

        void NavigateTo(string pageKey, byte[] parameter);

        void NavigateTo(string pageKey, string parameter, NavigationTransitionInfo navigationTransitionInfo);

        void NavigateTo(string pageKey, object parameter, NavigationTransitionInfo navigationTransitionInfo);

        bool NavigateTo<T>(object parameter = null) where T : Page;

        bool TryGoBack();

        void TryGoBack(NavigationTransitionInfo navigationTransitionInfo);

        void RemoveFromBackStack();
    }
}