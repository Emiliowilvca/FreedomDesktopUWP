using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ShellViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ShellViews
{
    public sealed partial class UrlBasePage : Page, IViewModel<UrlBaseViewModel>
    {
        public UrlBasePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<UrlBaseViewModel>();
        }

        public UrlBaseViewModel ViewModel { get; set; }
    }
}