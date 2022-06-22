using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class KardexReportPage : Page, IViewModel<KardexReportViewModel>
    {
        public KardexReportPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<KardexReportViewModel>();
        }

        public KardexReportViewModel ViewModel { get; set; }
    }
}