using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class ShopSearchPage : Page, IViewModel<ShopSearchViewModel>
    {
        public ShopSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ShopSearchViewModel>();
            DataContext = ViewModel;
        }

        public ShopSearchViewModel ViewModel { get; set; }
    }
}