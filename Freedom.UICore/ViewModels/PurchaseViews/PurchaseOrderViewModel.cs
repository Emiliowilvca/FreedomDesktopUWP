using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;
namespace Freedom.UICore.ViewModels.PurchaseViews
{
    public class PurchaseOrderViewModel : BaseViewModel
    {
        public PurchaseOrderViewModel()
        {
            PageTitle = new PageTitle(Lang.PurchaseOrder, MaterialDesignIcons.CartPlus);
        }
    }
}