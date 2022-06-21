using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class CitySearchPage : Page, IViewModel<CitySearchViewModel>
    {
        public CitySearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CitySearchViewModel>();
            DataContext = ViewModel;
        }

        public CitySearchViewModel ViewModel { get; set; }
    }
}