using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Freedom.UICore.ViewModels.VehicleViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.VehicleViews
{
    public sealed partial class VehiclePage : Page, IViewModel<VehicleViewModel>
    {
        public VehiclePage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<VehicleViewModel>();
        }

        public VehicleViewModel ViewModel { get; set; }
    }
}