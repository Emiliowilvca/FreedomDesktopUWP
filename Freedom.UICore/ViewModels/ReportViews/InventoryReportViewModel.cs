using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.Utility.Langs;

namespace Freedom.UICore.ViewModels.ReportViews
{
    public class InventoryReportViewModel : BaseViewModel
    {
        public InventoryReportViewModel()
        {
            PageTitle = new PageTitle(Lang.InventoryReport, MaterialDesignIcons.ChartTree);
        }
    }
}