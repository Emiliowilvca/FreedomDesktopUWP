using Freedom.UICore.BaseClass;
//using Freedom.UICore.ViewModels.AccountViews;
using Freedom.UICore.ViewModels.CountryViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.CountryViews
{
    public sealed partial class TownPage : Page, IViewModel<TownViewModel>
    {
        public TownPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<TownViewModel>();
        }

        public TownViewModel ViewModel { get; set; }
    }
}