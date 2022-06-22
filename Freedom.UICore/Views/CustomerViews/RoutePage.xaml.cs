using Freedom.UICore.BaseClass;
//using Freedom.UICore.ViewModels.AccountViews;
using Freedom.UICore.ViewModels.CustomerViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CustomerViews
{
    public sealed partial class RoutePage : Page, IViewModel<RouteViewModel>
    {
        public RoutePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<RouteViewModel>();
        }

        public RouteViewModel ViewModel { get; set; }
    }
}