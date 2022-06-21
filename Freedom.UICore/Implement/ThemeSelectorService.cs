using Freedom.UICore.Interface;
using Windows.UI.Xaml;

namespace Freedom.UICore.Implement
{
    public class ThemeSelectorService : IThemeSelectorService
    {
        public ElementTheme GetTheme()
        {
            if (AppEssential.MainWindow?.Content is FrameworkElement frameworkElement)
            {
                return frameworkElement.ActualTheme;
            }

            return ElementTheme.Default;
        }

        public void SetTheme(ElementTheme theme)
        {           
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = theme;
            }
        }
    }
}