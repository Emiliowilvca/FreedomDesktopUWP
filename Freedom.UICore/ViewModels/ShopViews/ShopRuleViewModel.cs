using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.ShopViews
{
    public class ShopRuleViewModel : BaseViewModel
    {
        public ShopRuleViewModel()
        {
            Title = Lang.ShopRule;
            PageTitle = new PageTitle(Lang.ShopRule, MaterialDesignIcons.Store);
        }
    }
}