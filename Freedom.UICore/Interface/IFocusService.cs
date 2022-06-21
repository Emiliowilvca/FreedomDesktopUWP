using Windows.UI.Xaml;

namespace Freedom.UICore.Interface
{
    public interface IFocusService
    {
        void SetTextboxFocus(string textboxName);

        void SetTextboxFocus(UIElement parent, string textboxName);

    }
}