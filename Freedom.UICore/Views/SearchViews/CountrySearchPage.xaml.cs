using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class CountrySearchPage : Page, IViewModel<CountrySearchViewModel>
    {
        public CountrySearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CountrySearchViewModel>();

            this.DataContext = ViewModel;   
        }

        public CountrySearchViewModel ViewModel { get; set; }
    }
}