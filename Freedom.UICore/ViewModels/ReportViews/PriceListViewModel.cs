using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.ReportViews
{
    public class PriceListViewModel : BaseViewModel
    {
        public PriceListViewModel()
        {
            Title = Lang.PriceList;
            PageTitle = new PageTitle(Lang.PriceList, MaterialDesignIcons.TextBoxMultipleOutline);
        }
    }
}