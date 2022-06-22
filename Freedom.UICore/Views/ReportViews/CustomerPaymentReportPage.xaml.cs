using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class CustomerPaymentReportPage : Page, IViewModel<CustomerPaymentReportViewModel>
    {
        public CustomerPaymentReportPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CustomerPaymentReportViewModel>();
        }

        public CustomerPaymentReportViewModel ViewModel { get; set; }
    }
}