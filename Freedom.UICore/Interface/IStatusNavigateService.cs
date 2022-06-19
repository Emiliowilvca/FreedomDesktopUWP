using Freedom.UICore.BaseClass;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Interface
{
    public interface IStatusNavigateService : IBaseNavigation
    {
        void InitializeStatusBar(Frame frame);

        void Navigate(string pageKey);
    }
}