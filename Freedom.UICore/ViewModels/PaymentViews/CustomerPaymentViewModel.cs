using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.PaymentViews
{
    public class CustomerPaymentViewModel : BaseViewModel
    {
        public CustomerPaymentViewModel()
        {
            Title = Lang.CustomerPayment;
            PageTitle = new PageTitle(Lang.CustomerPayment, MaterialDesignIcons.AccountCash);
        }
    }
}