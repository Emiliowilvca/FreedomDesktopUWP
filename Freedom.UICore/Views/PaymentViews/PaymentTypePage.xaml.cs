using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.PaymentViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.PaymentViews
{
    public sealed partial class PaymentTypePage : Page, IViewModel<PaymentTypeViewModel>
    {
        public PaymentTypePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PaymentTypeViewModel>();
        }

        public PaymentTypeViewModel ViewModel { get; set; }
    }
}