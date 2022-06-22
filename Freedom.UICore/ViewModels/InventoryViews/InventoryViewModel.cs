using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.Utility.Langs;

namespace Freedom.UICore.ViewModels.InventoryViews
{
    public class InventoryViewModel : BaseViewModel
    {
        public InventoryViewModel()
        {
            PageTitle = new PageTitle(Lang.Inventory, MaterialDesignIcons.PackageVariantClosed);
        }
    }
}