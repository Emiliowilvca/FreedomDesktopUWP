using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ShopViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ShopViews
{
    public sealed partial class ShopPage : Page, IViewModel<ShopViewModel>
    {
        public ShopPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ShopViewModel>();
        }

        public ShopViewModel ViewModel { get; set; }
    }
}