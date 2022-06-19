using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using Microsoft.UI.Xaml.Controls;
using System.Reflection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Implement
{
    public class StatusNavigateService : BaseNavigation, IStatusNavigateService
    {
        public StatusNavigateService(Assembly[] assemblies) : base(assemblies)
        {
        }

        public string GetCurrentPage()
        {
            return "";
        }

        public void InitializeStatusBar(Frame frame)
        {
            base.InitializeFrame(frame);
        }

        public void Navigate(string pageKey)
        {
            base.NavigateTo(pageKey, "");
        }
    }
}