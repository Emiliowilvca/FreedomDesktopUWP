using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;
namespace Freedom.UICore.ViewModels.ReportViews
{
    public class CustomerPaymentReportViewModel : BaseViewModel
    {
        public CustomerPaymentReportViewModel()
        {
            PageTitle = new PageTitle(Lang.CustomerPaymentReport, MaterialDesignIcons.ChartTimelineVariant);
        }
    }
}