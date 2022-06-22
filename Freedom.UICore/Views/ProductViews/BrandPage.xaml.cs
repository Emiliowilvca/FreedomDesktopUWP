using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class BrandPage : Page, IViewModel<BrandViewModel>
    {
        public BrandPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BrandViewModel>();
        }

        public BrandViewModel ViewModel { get; set; }
    }
}