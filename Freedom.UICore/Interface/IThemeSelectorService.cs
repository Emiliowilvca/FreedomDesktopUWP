using Windows.UI.Xaml;

namespace Freedom.UICore.Interface
{
    public interface IThemeSelectorService
    {
        ElementTheme GetTheme();

        void SetTheme(ElementTheme theme);
    }
}