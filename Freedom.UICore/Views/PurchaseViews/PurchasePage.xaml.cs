using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProviderViews;
using Freedom.UICore.ViewModels.PurchaseViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.PurchaseViews
{
    public sealed partial class PurchasePage : Page, IViewModel<PurchaseViewModel>
    {
        public PurchasePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PurchaseViewModel>();
        }

        public PurchaseViewModel ViewModel { get; set; }
    }
}