using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class SaleReportPage : Page, IViewModel<SaleReportViewModel>
    {
        public SaleReportPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<SaleReportViewModel>();
        }

        public SaleReportViewModel ViewModel { get; set; }
    }
}