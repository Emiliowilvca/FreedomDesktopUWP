using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.Utility.Langs;

namespace Freedom.UICore.ViewModels.ReportViews
{
    public class SaleReportViewModel : BaseViewModel
    {
        public SaleReportViewModel()
        {
            Title = Lang.SalesReport;
            PageTitle = new PageTitle(Lang.SalesReport, MaterialDesignIcons.ChartBar);
        }
    }
}