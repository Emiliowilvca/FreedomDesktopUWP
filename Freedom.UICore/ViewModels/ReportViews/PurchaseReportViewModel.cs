using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;
namespace Freedom.UICore.ViewModels.ReportViews
{
    public class PurchaseReportViewModel : BaseViewModel
    {
        public PurchaseReportViewModel()
        {
            PageTitle = new PageTitle(Lang.PurchaseReport, MaterialDesignIcons.ArchiveClock);
        }
    }
}