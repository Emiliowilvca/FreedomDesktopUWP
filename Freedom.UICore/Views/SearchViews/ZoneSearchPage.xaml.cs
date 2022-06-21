using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class ZoneSearchPage : Page, IViewModel<ZoneSearchViewModel>
    {
        public ZoneSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ZoneSearchViewModel>();
            DataContext = ViewModel;
        }

        public ZoneSearchViewModel ViewModel { get; set; }
    }
}