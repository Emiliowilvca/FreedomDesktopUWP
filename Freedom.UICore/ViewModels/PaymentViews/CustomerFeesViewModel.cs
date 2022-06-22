using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.Utility.Langs;

namespace Freedom.UICore.ViewModels.PaymentViews
{
    public class CustomerFeesViewModel : BaseViewModel
    {
        public CustomerFeesViewModel()
        {
            Title = Lang.CustomerFees;
            PageTitle = new PageTitle(Lang.CustomerFees, MaterialDesignIcons.AccountClockOutline);
        }
    }
}