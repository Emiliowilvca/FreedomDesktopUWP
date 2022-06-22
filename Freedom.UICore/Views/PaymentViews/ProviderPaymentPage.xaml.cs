using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.EmployeeViews;
using Freedom.UICore.ViewModels.MainViews;
using Freedom.UICore.ViewModels.PaymentViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.PaymentViews
{
    public sealed partial class ProviderPaymentPage : Page, IViewModel<ProviderPaymentViewModel>
    {
        public ProviderPaymentPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ProviderPaymentViewModel>();
        }

        public ProviderPaymentViewModel ViewModel { get; set; }
    }
}