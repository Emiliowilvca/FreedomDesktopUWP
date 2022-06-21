using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using System;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Freedom.UICore.Implement
{
    public class NavigationService : BaseNavigation, INavigationService
    {
        public Action<string> NavigateToAction { get; set; }

        public NavigationService(Assembly[] viewsAssemblies) : base(viewsAssemblies)
        {

        }

        public void InitializeNavigationContainer(Frame frame)
        {
            base.InitializeFrame(frame);
        }

        public void GoBack()
        {
            base.TryGoBack();
        }

        public void GoBack(NavigationTransitionInfo transitionInfo)
        {
            base.TryGoBack(transitionInfo);
        }
    }
}