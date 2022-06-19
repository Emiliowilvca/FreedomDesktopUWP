using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Freedom.UICore.Interface
{
    public interface IShellNavigationService
    {
        void Initialize(Frame shellFrame);

        bool IsShell();

        bool CanGoBack();

        bool CanGoForward();

        bool GoBack();

        void GoForward();

        void RemoveFromBackStack();

        bool Navigate(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null);

        bool Navigate<T>(object parameter = null, NavigationTransitionInfo infoOverride = null) where T : Page;
    }
}