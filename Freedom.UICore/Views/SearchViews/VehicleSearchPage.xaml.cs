using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class VehicleSearchPage : Page, IViewModel<VehicleSearchViewModel>
    {
        public VehicleSearchPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<VehicleSearchViewModel>();
            DataContext = ViewModel;
        }

        public VehicleSearchViewModel ViewModel { get; set; }
    }
}