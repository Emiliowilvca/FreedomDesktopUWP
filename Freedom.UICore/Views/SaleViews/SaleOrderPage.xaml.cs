using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SaleViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SaleViews
{
    public sealed partial class SaleOrderPage : Page, IViewModel<SaleOrderViewModel>
    {
        public SaleOrderPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<SaleOrderViewModel>();
        }

        public SaleOrderViewModel ViewModel { get; set; }
    }
}