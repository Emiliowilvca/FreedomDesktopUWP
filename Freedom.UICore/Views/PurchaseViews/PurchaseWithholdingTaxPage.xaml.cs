using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProviderViews;
using Freedom.UICore.ViewModels.PurchaseViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.PurchaseViews
{
    public sealed partial class PurchaseWithholdingTaxPage : Page, IViewModel<PurchaseWithholdingTaxViewModel>
    {
        public PurchaseWithholdingTaxPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PurchaseWithholdingTaxViewModel>();
        }

        public PurchaseWithholdingTaxViewModel ViewModel { get; set; }
    }
}