using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class ProductPage : Page, IViewModel<ProductViewModel>
    {
        public ProductPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ProductViewModel>();
        }

        public ProductViewModel ViewModel { get; set; }
    }
}