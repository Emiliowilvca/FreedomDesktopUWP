using Freedom.UICore.BaseClass;
//using Freedom.UICore.ViewModels.AccountViews;
using Freedom.UICore.ViewModels.CustomerViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CustomerViews
{
    public sealed partial class ZonePage : Page, IViewModel<ZoneViewModel>
    {
        public ZonePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ZoneViewModel>();
        }

        public ZoneViewModel ViewModel { get; set; }
    }
}