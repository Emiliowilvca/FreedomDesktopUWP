using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.ReportViews
{
    public class ProviderPaymentReportViewModel : BaseViewModel
    {
        public ProviderPaymentReportViewModel()
        {
            PageTitle = new PageTitle(Lang.ProviderPaymentReport, MaterialDesignIcons.ChartBoxOutline);
        }
    }
}