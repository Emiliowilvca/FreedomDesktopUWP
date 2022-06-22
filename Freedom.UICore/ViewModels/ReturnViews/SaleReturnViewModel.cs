using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.ReturnViews
{
    public class SaleReturnViewModel : BaseViewModel
    {
        public SaleReturnViewModel()
        {
            Title = Lang.SalesReturn;
            PageTitle = new PageTitle(Lang.SalesReturn, MaterialDesignIcons.CartRemove);
        }
    }
}