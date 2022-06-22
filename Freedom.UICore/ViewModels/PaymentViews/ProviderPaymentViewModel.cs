using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.PaymentViews
{
    public class ProviderPaymentViewModel : BaseViewModel
    {
        public ProviderPaymentViewModel()
        {
            Title = Lang.ProviderPayment;
            PageTitle = new PageTitle(Lang.ProviderPayment, MaterialDesignIcons.CalendarTextOutline);
        }
    }
}