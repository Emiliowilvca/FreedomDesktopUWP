using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProviderViews;
using Freedom.UICore.ViewModels.PurchaseViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.PurchaseViews
{
    public sealed partial class PurchaseOrderPage : Page, IViewModel<PurchaseOrderViewModel>
    {
        public PurchaseOrderPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PurchaseOrderViewModel>();
        }

        public PurchaseOrderViewModel ViewModel { get; set; }
    }
}