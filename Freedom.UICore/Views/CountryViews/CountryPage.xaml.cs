using Freedom.UICore.BaseClass;
//using Freedom.UICore.ViewModels.AccountViews;
using Freedom.UICore.ViewModels.CountryViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.CountryViews
{
    public sealed partial class CountryPage : Page, IViewModel<CountryViewModel>
    {
        public CountryPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CountryViewModel>();
        }

        public CountryViewModel ViewModel { get; set; }
    }
}