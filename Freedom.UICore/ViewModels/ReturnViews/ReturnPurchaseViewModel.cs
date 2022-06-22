using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.ReturnViews
{
    public class ReturnPurchaseViewModel : BaseViewModel
    {
        public ReturnPurchaseViewModel()
        {
            Title = Lang.ReturnPurchases;
            PageTitle = new PageTitle(Lang.ReturnPurchases, MaterialDesignIcons.BasketRemove);
        }
    }
}