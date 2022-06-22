using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReturnViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ReturnViews
{
    public sealed partial class ReturnPurchasePage : Page, IViewModel<ReturnPurchaseViewModel>
    {
        public ReturnPurchasePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ReturnPurchaseViewModel>();
        }

        public ReturnPurchaseViewModel ViewModel { get; set; }
    }
}