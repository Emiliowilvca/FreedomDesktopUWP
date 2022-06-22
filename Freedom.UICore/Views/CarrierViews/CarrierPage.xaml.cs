using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.CarrierViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CarrierViews
{
    public sealed partial class CarrierPage : Page, IViewModel<CarrierViewModel>
    {
        public CarrierPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetService<CarrierViewModel>();
        }

        public CarrierViewModel ViewModel { get; set; }
    }
}