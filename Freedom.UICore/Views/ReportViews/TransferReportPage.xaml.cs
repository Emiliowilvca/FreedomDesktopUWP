using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class TransferReportPage : Page, IViewModel<TransferReportViewModel>
    {
        public TransferReportPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<TransferReportViewModel>();
        }

        public TransferReportViewModel ViewModel { get; set; }
    }
}