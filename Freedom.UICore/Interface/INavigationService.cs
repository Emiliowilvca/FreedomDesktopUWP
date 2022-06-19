using Freedom.UICore.BaseClass;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Freedom.UICore.Interface
{
    public interface INavigationService : IBaseNavigation
    {
        //  void InitializeNavigationContainer(string frameName);

        void InitializeNavigationContainer(Frame frame);

        void GoBack();

        void GoBack(NavigationTransitionInfo transitionInfo);
    }
}