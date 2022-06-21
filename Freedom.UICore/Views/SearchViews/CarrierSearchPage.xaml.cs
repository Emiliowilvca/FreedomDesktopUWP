using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class CarrierSearchPage : Page, IViewModel<CarrierSearchViewModel>
    {
        public CarrierSearchPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CarrierSearchViewModel>();
            DataContext = ViewModel;
        }

        public CarrierSearchViewModel ViewModel { get; set; }
    }
}
