using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;
namespace Freedom.UICore.ViewModels.PurchaseViews
{
    public class PurchaseViewModel : BaseViewModel
    {
        public PurchaseViewModel()
        {
            PageTitle = new PageTitle(Lang.Purchase, MaterialDesignIcons.TruckDeliveryOutline);
        }
    }
}