using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class ProductPricePage : Page, IViewModel<ProductPriceViewModel>
    {
        public ProductPricePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ProductPriceViewModel>();
        }

        public ProductPriceViewModel ViewModel { get; set; }
    }
}