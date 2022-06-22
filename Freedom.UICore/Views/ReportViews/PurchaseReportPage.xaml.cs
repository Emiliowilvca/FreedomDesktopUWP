using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class PurchaseReportPage : Page, IViewModel<PurchaseReportViewModel>
    {
        public PurchaseReportPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PurchaseReportViewModel>();
        }

        public PurchaseReportViewModel ViewModel { get; set; }
    }
}