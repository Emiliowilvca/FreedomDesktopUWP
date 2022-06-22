using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Freedom.UICore.ViewModels.ShopViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ShopViews
{
    public sealed partial class ShopRulePage : Page, IViewModel<ShopRuleViewModel>
    {
        public ShopRulePage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ShopRuleViewModel>();
        }

        public ShopRuleViewModel ViewModel { get; set; }
    }
}