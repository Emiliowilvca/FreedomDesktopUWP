using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class ProviderFeesReportPage : Page, IViewModel<ProviderFeesReportViewModel>
    {
        public ProviderFeesReportPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ProviderFeesReportViewModel>();
        }

        public ProviderFeesReportViewModel ViewModel { get; set; }
    }
}