using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.ReportViews
{
    public class TransferReportViewModel : BaseViewModel
    {
        public TransferReportViewModel()
        {
            Title = Lang.TransferReport;
            PageTitle = new PageTitle(Lang.TransferReport, MaterialDesignIcons.FileSwap);
        }
    }
}