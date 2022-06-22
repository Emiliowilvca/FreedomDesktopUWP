using Freedom.UICore.BaseClass;
//using Freedom.UICore.ViewModels.AccountViews;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class BankDepositReportPage : Page, IViewModel<BankDepositReportViewModel>
    {
        public BankDepositReportPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetService<BankDepositReportViewModel>();
        }

        public BankDepositReportViewModel ViewModel { get; set; }
    }
}