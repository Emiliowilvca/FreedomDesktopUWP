using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;
namespace Freedom.UICore.ViewModels.ReportViews
{
    public class OrderNotDeliveredViewModel : BaseViewModel
    {
        public OrderNotDeliveredViewModel()
        {
            PageTitle = new PageTitle(Lang.UnfulfilledOrders, MaterialDesignIcons.CartMinus);
        }
    }
}