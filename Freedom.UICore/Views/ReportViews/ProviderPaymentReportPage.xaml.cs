using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class ProviderPaymentReportPage : Page, IViewModel<ProviderPaymentReportViewModel>
    {
        public ProviderPaymentReportPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ProviderPaymentReportViewModel>();
        }

        public ProviderPaymentReportViewModel ViewModel { get; set; }
    }
}