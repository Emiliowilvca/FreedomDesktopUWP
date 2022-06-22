using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.ReportViews
{
    public class KardexReportViewModel : BaseViewModel
    {
        public KardexReportViewModel()
        {
            PageTitle = new PageTitle(Lang.Kardex, MaterialDesignIcons.ClipboardTextSearchOutline);
        }
    }
}