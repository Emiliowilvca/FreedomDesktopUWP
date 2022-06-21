using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankExtractionReportPage : Page, IViewModel<BankExtractionReportViewModel>
    {
        public BankExtractionReportPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BankExtractionReportViewModel>();
        }

        public BankExtractionReportViewModel ViewModel { get; set; }
    }
}