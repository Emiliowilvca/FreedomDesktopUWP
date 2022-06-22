using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.CountryViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CountryViews
{
    public sealed partial class CityPage : Page, IViewModel<CityViewModel>
    {
        public CityPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CityViewModel>();
        }

        public CityViewModel ViewModel { get; set; }
    }
}