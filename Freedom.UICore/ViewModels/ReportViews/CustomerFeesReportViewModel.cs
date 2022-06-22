using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;
namespace Freedom.UICore.ViewModels.ReportViews
{
    public class CustomerFeesReportViewModel : BaseViewModel
    {
        public CustomerFeesReportViewModel()
        {
            PageTitle = new PageTitle(Lang.CustomerDebtsReport, MaterialDesignIcons.AccountClockOutline);
        }
    }
}