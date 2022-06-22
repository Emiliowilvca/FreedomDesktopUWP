using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class CustomerFeesReportPage : Page, IViewModel<CustomerFeesReportViewModel>
    {
        public CustomerFeesReportPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CustomerFeesReportViewModel>();
        }

        public CustomerFeesReportViewModel ViewModel { get; set; }
    }
}